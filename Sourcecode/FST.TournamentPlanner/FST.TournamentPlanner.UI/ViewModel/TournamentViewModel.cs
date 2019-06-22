using FST.TournamentPlanner.UI.ViewModel.Messages;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Device.Location;
using System.Globalization;
using FST.TournamentPlanner.UI.ViewModel.Models;
using System.Net.Http;
using msg = FST.TournamentPlanner.UI.ViewModel.Messages;

namespace FST.TournamentPlanner.UI.ViewModel
{
    /// <summary>
    /// Viewmodel representing a tournament
    /// </summary>
    public class TournamentViewModel : ViewModelBase<Model.Models.Tournament>
    {
        /// <summary>
        /// Tournament is in created state
        /// </summary>
        public const int STATE_CREATED = 0;

        /// <summary>
        /// Tournament is in started state
        /// </summary>
        public const int STATE_STARTED = 1;

        /// <summary>
        /// Tournament is in finished state
        /// </summary>
        public const int STATE_FINISHED = 2;

        private GeoCoordinateWatcher _watcher;
        private double _latitude;
        private double _longitude;

        /// <summary>
        /// Constructor
        /// </summary>
        public TournamentViewModel(Model.Models.Tournament tournament) : base(tournament)
        {
            tournament.PlayAreas.ToList().ForEach(p => PlayAreas.Add(ViewModelLocator.Instance.GetPlayAreaViewModel(Model, p)));
            UpdateFromModel(tournament);

            _watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
            _watcher.PositionChanged += this.Watcher_PositionChanged;
            _watcher.StatusChanged += this.Watcher_StatusChanged;
            _watcher.Start();


            MessengerInstance.Register<msg.TournamentFinishedMessage>(this, (m) =>
            {
                if (m.TournamentId == Model.Id.Value)
                {
                    try
                    {
                        var resp = App.RestClient.GetWithHttpMessagesAsync(Id);
                        resp.ContinueWith(t =>
                        {
                            Model = resp.Result.Body;
                            UpdateFromModel(Model);
                        });
                    }
                    catch (Exception e)
                    {
                        if (e.GetType() == typeof(AggregateException) || e.GetType() == typeof(Microsoft.Rest.HttpOperationException))
                        {
                            MessengerInstance.Send(new CommunicationErrorMessage());
                        }
                        else
                        {
                            throw e;
                        }
                    }
                }
            });

        }

        private void UpdateFromModel(Model.Models.Tournament tournament)
        {
            //Set values
            _name = tournament.Name;
            _startDate = tournament.StartTime ?? DateTime.Now;
            _description = tournament.Description;
            _maximumMatchDururationInMinutes = tournament.MatchDuration ?? 30;
            _teamCount = Model.TeamCount ?? 16;

            //Raise property changed for values
            RaisePropertyChanged(() => Name);
            RaisePropertyChanged(() => Description);
            RaisePropertyChanged(() => MaximumMatchDurationInMinutes);
            RaisePropertyChanged(() => TeamCount);

            //Raise property changed for calculated values
            RaisePropertyChanged(() => StartDate);
            RaisePropertyChanged(() => StartTime);
            RaisePropertyChanged(() => State);
            RaisePropertyChanged(() => FinalMatch);
            RaisePropertyChanged(() => Matches);
            RaisePropertyChanged(() => HasChanges);
            RaisePropertyChanged(() => TournamentEditable);

            //Raise property changed for commands
            SaveChangesCommand.RaiseCanExecuteChanged();
            StartCommand.RaiseCanExecuteChanged();
            AddTeamCommand.RaiseCanExecuteChanged();
            RemoveTeamCommand.RaiseCanExecuteChanged();
            AddPlayAreaCommand.RaiseCanExecuteChanged();
            RemovePlayAreaCommand.RaiseCanExecuteChanged();
            GenerateWinnerCertificatesCommand.RaiseCanExecuteChanged();
        }

        #region Teams
        private ObservableCollection<TeamViewModel> _teams;
        public ObservableCollection<TeamViewModel> Teams
        {
            get
            {
                if (_teams == null)
                {
                    _teams = new ObservableCollection<TeamViewModel>(Model.Teams.Select(t => ViewModelLocator.Instance.GetTeamViewModel(Model, t)));
                }
                return _teams;
            }
        }
        #endregion

        #region MasterData

        #region Id
        /// <summary>
        /// Id of the tournament
        /// </summary>
        public Int32 Id
        {
            get
            {
                return Model.Id.Value;
            }
        }
        #endregion

        #region TournamentMode
        /// <summary>
        /// Describes the mode in which the tournament is held.
        /// In the first release only the knock-out mode is supported
        /// </summary>
        public TournamentMode TournamentMode
        {
            get
            {
                return TournamentMode.KnockOut;
            }
        }
        #endregion

        #region Name
        private string _name;
        public string Name
        {
            set
            {
                if (State != STATE_CREATED)
                {
                    MessengerInstance.Send(new Messages.TournamentAllreadyStarted(Model.Id.Value));
                }
                else
                {
                    _name = value;
                }
                RaisePropertyChanged(() => Name);
                RaisePropertyChanged(() => Title);
                RaisePropertyChanged(() => HasChanges);
            }
            get
            {
                return _name;
            }
        }
        #endregion

        #region StartDate
        private DateTime _startDate;
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                if (State != STATE_CREATED)
                {
                    MessengerInstance.Send(new Messages.TournamentAllreadyStarted(Model.Id.Value));
                }
                else
                {
                    if (_startDate.Date != value.Date)
                    {
                        _startDate = value.Add(_startDate.TimeOfDay);
                    }
                }
                RaisePropertyChanged(() => StartDate);
                RaisePropertyChanged(() => HasChanges);
            }
        }
        #endregion

        #region StartTime
        public DateTime StartTime
        {
            get
            {
                return _startDate;
            }
            set
            {
                if (State != STATE_CREATED)
                {
                    MessengerInstance.Send(new Messages.TournamentAllreadyStarted(Model.Id.Value));
                }
                else
                {
                    if (_startDate.TimeOfDay != value.TimeOfDay)
                    {
                        _startDate = _startDate.Date.Add(value.TimeOfDay);
                    }
                }
                RaisePropertyChanged(() => StartTime);
                RaisePropertyChanged(() => HasChanges);
            }
        }
        #endregion

        #region Description
        private string _description;
        public string Description
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_description))
                {
                    return string.Empty;
                }
                return _description;
            }
            set
            {
                if (_description == null && (_description == null || _description.Equals(value)))
                {
                    return;
                }
                if (State != STATE_CREATED)
                {
                    MessengerInstance.Send(new Messages.TournamentAllreadyStarted(Model.Id.Value));
                }
                else
                {
                    _description = value;
                }
                RaisePropertyChanged(() => Description);
                RaisePropertyChanged(() => HasChanges);
            }
        }
        #endregion

        #region MaximumMatchDurationInMinutes
        public int _maximumMatchDururationInMinutes;
        public int MaximumMatchDurationInMinutes
        {
            get
            {
                return _maximumMatchDururationInMinutes;
            }
            set
            {
                if (State != STATE_CREATED)
                {
                    MessengerInstance.Send(new Messages.TournamentAllreadyStarted(Model.Id.Value));
                }
                else
                {
                    _maximumMatchDururationInMinutes = value;
                }
                RaisePropertyChanged(() => MaximumMatchDurationInMinutes);
                RaisePropertyChanged(() => HasChanges);
            }
        }
        #endregion

        #region TeamCount
        private int _teamCount;
        public int TeamCount
        {
            get
            {
                return _teamCount;
            }
            set
            {
                if (State != STATE_CREATED)
                {
                    MessengerInstance.Send(new Messages.TournamentAllreadyStarted(Model.Id.Value));
                }
                else
                {
                    _teamCount = value;
                }
                RaisePropertyChanged(() => TeamCount);
                RaisePropertyChanged(() => HasChanges);
            }
        }

        #endregion

        #endregion

        #region State
        /// <summary>
        /// State of the tournament
        /// </summary>
        public int State
        {
            get
            {
                return Model.State.Value;
            }
        }

        #endregion

        #region PlayAreas
        private ObservableCollection<PlayAreaViewModel> _playAreas = new ObservableCollection<PlayAreaViewModel>();
        /// <summary>
        /// Play area list of the tournament
        /// </summary>
        public ObservableCollection<PlayAreaViewModel> PlayAreas
        {
            get
            {
                return _playAreas;
            }
        }
        #endregion

        #region TeamCountChoises
        /// <summary>
        /// Maximum number of teams within the tournament
        /// </summary>
        public List<int> TeamCountChoises => new List<int>() { 4, 8, 16, 32, 64 };
        #endregion

        #region FinalMatch
        public List<MatchViewModel> FinalMatch
        {
            get
            {
                if (Model.FinalMatch == null)
                {
                    return new List<MatchViewModel>();
                }
                return new List<MatchViewModel>() { ViewModelLocator.Instance.GetMatchViewModel(Model, Model.FinalMatch, null) };
            }
        }
        #endregion

        #region Matches
        public ObservableCollection<MatchViewModel> Matches
        {
            get
            {
                var res = new ObservableCollection<MatchViewModel>();
                if (FinalMatch.Count() == 0)
                {
                    return res;
                }
                MatchesRecursion(res, FinalMatch.First());
                return res;
            }
        }
        private void MatchesRecursion(ObservableCollection<MatchViewModel> result, MatchViewModel match)
        {
            result.Add(match);
            match.Predecessors.ForEach(p => MatchesRecursion(result, p));
        }
        #endregion

        #region Avalon Dock Stuff

        public string Title => Name;


        #region CanClose
        public bool CanClose
        {
            get
            {
                return true;
            }
        }
        #endregion  

        #region CloseCommand
        private RelayCommand _closeCommand;
        public RelayCommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(() => 
                    {
                        Close(true);
                    }, CanClose);
                }
                return _closeCommand;
            }
        }
        internal void Close(bool askSaveChanges)
        {
            if (HasChanges && askSaveChanges)
            {
                MessengerInstance.Send(new AreYouSureMessage("Änderungen", "Sollen die Änderungen gespeichert werden?", () =>
                {
                    //Save changes
                    SaveChangesCommand.Execute(null);
                }));
            }
            MessengerInstance.Send(new CloseTournamentMessage(this));
        }
        #endregion

        #endregion

        #region HasChanges
        public bool HasChanges
        {
            get
            {
                return
                    Model.Name != Name ||
                    Model.Description != Description ||
                    Model.TeamCount != TeamCount ||
                    Model.StartTime != _startDate ||
                    Model.MatchDuration != _maximumMatchDururationInMinutes;
            }
        }
        #endregion

        #region SaveChangesCommand
        private RelayCommand _saveChangesCommand;
        public RelayCommand SaveChangesCommand
        {
            get
            {
                if (_saveChangesCommand == null)
                {
                    _saveChangesCommand = new RelayCommand(async () =>
                    {
                        try
                        {
                            Model = App.RestClient.UpdateTournamentWithHttpMessagesAsync(Model.Id.Value,
                                new Model.Models.Tournament(Model.Id.Value,
                                Name,
                                Description,
                                _startDate,
                                MaximumMatchDurationInMinutes,
                                TeamCount,
                                State,
                                PlayAreas.Select(a => a.Model).ToList(),
                                Teams.Select(t => t.Model).ToList())).Result.Body;
                            UpdateFromModel(Model);
                        }
                        catch (Exception e)
                        {
                            if (e.GetType() == typeof(AggregateException) || e.GetType() == typeof(Microsoft.Rest.HttpOperationException))
                            {
                                MessengerInstance.Send(new CommunicationErrorMessage());
                            }
                            else
                            {
                                throw e;
                            }
                        }
                    }, () => HasChanges);
                }
                return _saveChangesCommand;
            }
        } 
        #endregion

        #region TournamentEditable
        public bool TournamentEditable
        {
            get
            {
                return State == STATE_CREATED;
            }
        }
        #endregion

        #region StartCommand
        private RelayCommand _startCommand;
        public RelayCommand StartCommand
        {
            get
            {
                if (_startCommand == null)
                {
                    _startCommand = new RelayCommand(() =>
                    {
                        Start();
                    }, () => State == STATE_CREATED);
                }
                return _startCommand;
            }
        }
        private void Start()
        {
            if (PlayAreas.Count == 0)
            {
                MessengerInstance.Send(new NoPlayAreaDefinedMessage());
                return;
            }
            if (TeamCount != Teams.Count())
            {
                MessengerInstance.Send(new TeamCountMismatchMessage(Model.Id.Value, Teams.Count, TeamCount));
                return;
            }
            if (HasChanges)
            {
                bool abortStart = true;
                MessengerInstance.Send(new AreYouSureMessage("Ungespeicherte Änderungen", "Es gibt noch ungespeicherte Änderungen.\nDiese müssen gespeichert werden vor dem Start.\nWollen Sie jetzt speichern und fortfahren?", () =>
                {
                    abortStart = false;
                }));
                if (abortStart)
                {
                    return;
                }
                SaveChangesCommand.Execute(null);
            }
            try
            {
                var resp = App.RestClient.StartWithHttpMessagesAsync(Id);
                resp.ContinueWith(t =>
                {
                    Model = resp.Result.Body;
                    UpdateFromModel(Model);
                }
                );
                
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(AggregateException) || e.GetType() == typeof(Microsoft.Rest.HttpOperationException))
                {
                    MessengerInstance.Send(new CommunicationErrorMessage());
                }
                else
                {
                    throw e;
                }
            }
            // Alle Properties => RaisePropertyChanged / Command.CanExecute updaten

        }
        #endregion

        #region SelectedPlayArea
        private PlayAreaViewModel _selectePlayArea;
        public PlayAreaViewModel SelectedPlayArea
        {
            get
            {
                return _selectePlayArea;
            }
            set
            {
                if ((_selectePlayArea == null && value == null) || (_selectePlayArea != null && _selectePlayArea.Equals(value)))
                {
                    return;
                }
                _selectePlayArea = value;
                RaisePropertyChanged(() => SelectedPlayArea);
            }
        }
        #endregion  

        #region SelectedTeam
        private TeamViewModel _selectedTeam;
        public TeamViewModel SelectedTeam
        {
            get
            {
                return _selectedTeam;
            }
            set
            {
                if ((_selectedTeam == null && value == null) || (_selectedTeam != null && _selectedTeam.Equals(value)))
                {
                    return;
                }
                _selectedTeam = value;
                RaisePropertyChanged(() => SelectedTeam);
            }
        }
        #endregion

        #region AddTeamCommand
        private RelayCommand _addTeamCommand;
        public RelayCommand AddTeamCommand
        {
            get
            {
                if (_addTeamCommand == null)
                {
                    _addTeamCommand = new RelayCommand(() =>
                    {
                        if (Teams.Count < TeamCount)
                        {
                            try
                            {
                                var teamModel = App.RestClient.AddTeamWithHttpMessagesAsync(Model.Id.Value, "Neues Team").Result.Body;
                                var teamViewModel = ViewModelLocator.Instance.GetTeamViewModel(Model, teamModel);
                                Teams.Add(teamViewModel);
                                SelectedTeam = teamViewModel;
                            }
                            catch (Exception e)
                            {
                                if (e.GetType() == typeof(AggregateException) || e.GetType() == typeof(Microsoft.Rest.HttpOperationException))
                                {
                                    MessengerInstance.Send(new CommunicationErrorMessage());
                                }
                                else
                                {
                                    throw e;
                                }
                            }
                        }
                        else
                        {
                            MessengerInstance.Send(new MaximumTeamCountReachedMessage());
                        }
                    }, () => State == STATE_CREATED);
                }
                return _addTeamCommand;
            }
        }
        #endregion

        #region RemoveTeamCommand
        private RelayCommand _removeTeamCommand;
        public RelayCommand RemoveTeamCommand
        {
            get
            {
                if (_removeTeamCommand == null)
                {
                    _removeTeamCommand = new RelayCommand(async () =>
                    {
                        if (SelectedTeam == null)
                        {
                            return;
                        }
                        await App.RestClient.RemoveTeamWithHttpMessagesAsync(Model.Id.Value, SelectedTeam.Id.Value);

                        int index = Teams.IndexOf(SelectedTeam);
                        Teams.Remove(SelectedTeam);
                        //select an other team from the list as selected
                        if (Teams.Count > 0)
                        {
                            if (index > 0)
                            {
                                SelectedTeam = Teams[index - 1];
                            }
                            else
                            {
                                SelectedTeam = Teams[index];
                            }
                            return;
                        }
                        SelectedTeam = null;
                    }, () => State == STATE_CREATED && SelectedTeam != null);
                }
                return _removeTeamCommand;
            }
        }
        #endregion

        #region AddPlayAreaCommand
        private RelayCommand _addPlayAreaCommand;
        public RelayCommand AddPlayAreaCommand
        {
            get
            {
                if (_addPlayAreaCommand == null)
                {
                    _addPlayAreaCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            Model.Models.PlayArea playArea = App.RestClient.AddPlayAreaWithHttpMessagesAsync(Model.Id.Value, "Neues Spielfeld", "Beschreibung hier einfügen").Result.Body;
                            var playAreaViewModel = ViewModelLocator.Instance.GetPlayAreaViewModel(Model, playArea);
                            PlayAreas.Add(playAreaViewModel);
                            SelectedPlayArea = playAreaViewModel;
                        }
                        catch (Exception e)
                        {
                            if (e.GetType() == typeof(AggregateException) || e.GetType() == typeof(Microsoft.Rest.HttpOperationException))
                            {
                                MessengerInstance.Send(new CommunicationErrorMessage());
                            }
                            else
                            {
                                throw e;
                            }
                        }

                    }, () => State == STATE_CREATED);
                }
                return _addPlayAreaCommand;
            }
        }
        #endregion

        #region RemovePlayAreaCommand
        private RelayCommand _removePlayAreaCommand;
        public RelayCommand RemovePlayAreaCommand
        {
            get
            {
                if (_removePlayAreaCommand == null)
                {
                    _removePlayAreaCommand = new RelayCommand(async () =>
                    {
                        if (SelectedPlayArea == null)
                        {
                            return;
                        }
                        await App.RestClient.RemovePlayAreaWithHttpMessagesAsync(Model.Id.Value, SelectedPlayArea.Id);
                        int index = PlayAreas.IndexOf(SelectedPlayArea);
                        PlayAreas.Remove(SelectedPlayArea);
                        //select an other playarea from the list as selected
                        if (PlayAreas.Count > 0)
                        {
                            if (index > 0)
                            {
                                SelectedPlayArea = PlayAreas[index - 1];
                            }
                            else
                            {
                                SelectedPlayArea = PlayAreas[index];
                            }
                            return;
                        }
                        SelectedPlayArea = null;
                    }, () => State == STATE_CREATED && SelectedPlayArea != null);
                }
                return _removePlayAreaCommand;
            }
        }
        #endregion

        #region GenerateWinnerCertificatesCommand
        private RelayCommand _generateWinnerCertificatesCommand;
        public RelayCommand GenerateWinnerCertificatesCommand
        {
            get
            {
                if (_generateWinnerCertificatesCommand == null)
                {
                    _generateWinnerCertificatesCommand = new RelayCommand(() =>
                    {
                        GenerateWinnerCertificates();
                    },
                    () => State == STATE_FINISHED);
                }
                return _generateWinnerCertificatesCommand;
            }
        }
        private void GenerateWinnerCertificates()
        {
            var finalMatch = this.FinalMatch.FirstOrDefault();
            string winner = FinalMatch.First().Winner.Name;
            string looser = FinalMatch.First().Looser.Name;

            if (finalMatch != null && finalMatch.Winner != null && finalMatch.Looser != null)
            {
                winner = finalMatch.Winner.Name;
                looser = finalMatch.Looser.Name;
            }

            MessengerInstance.Send(new GenerateWinnerCertificatesMessage(winner, looser, this.Name, GetStreetAddressForCoordinates(), DateTime.Now));
        }

        private void Watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            GeoCoordinate coordinate = _watcher.Position.Location;
            if (!coordinate.IsUnknown)
            {
                _latitude = coordinate.Latitude;
                _longitude = coordinate.Longitude;
            }
        }

        private void Watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            GeoCoordinate coordinate = _watcher.Position.Location;
            if (!coordinate.IsUnknown)
            {
                _latitude = coordinate.Latitude;
                _longitude = coordinate.Longitude;
            }
        }
        
        private string GetStreetAddressForCoordinates()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://nominatim.openstreetmap.org");
            httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            httpClient.DefaultRequestHeaders.Add("Referer", "http://www.microsoft.com");

            HttpResponseMessage httpResult = httpClient.GetAsync(String.Format("reverse?format=json&lat={0}&lon={1}", _latitude.ToString("G", CultureInfo.InvariantCulture), _longitude.ToString("G", CultureInfo.InvariantCulture))).Result;

            string jsonData = httpResult.Content.ReadAsStringAsync().Result;

            LocationObject rootObject = Newtonsoft.Json.JsonConvert.DeserializeObject<LocationObject>(jsonData);
            if (rootObject.address.city != null)
            {
                return rootObject.address.city;
            }

            return rootObject.address.village;
        }
        #endregion

        
    }
}
