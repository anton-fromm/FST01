using FST.TournamentPlanner.UI.ViewModel.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace FST.TournamentPlanner.UI.ViewModel
{
    /// <summary>
    /// ViewModel of the main window
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
                CurrentDocument = new TournamentViewModel(CreateDesignTimeVm());
                OpenedDocuments.Add(CurrentDocument);
            }
            else
            {
                new List<Model.Models.Tournament>(App.RestClient.GetAllWithHttpMessagesAsync().Result.Body).ForEach(t => Tournaments.Add(new TournamentViewModel(t)));
            }

            MessengerInstance.Register<OpenTournamentMessage>(this, m =>
            {
                OpenedDocuments.Add(m.Tournament);
                CurrentDocument = m.Tournament;
            });
            MessengerInstance.Register<CloseTournamentMessage>(this, m =>
            {
                if (OpenedDocuments.Contains(m.Sender))
                {
                    CurrentDocument = OpenedDocuments[0];
                    OpenedDocuments.Remove(m.Sender);
                }
            });

        }

        #region Tournaments
        private ObservableCollection<TournamentViewModel> _tournaments;
        /// <summary>
        /// List of tournaments
        /// </summary>
        public ObservableCollection<TournamentViewModel> Tournaments
        {
            get
            {
                if (_tournaments == null)
                {
                    _tournaments = new ObservableCollection<TournamentViewModel>();
                }
                return _tournaments;
            }
        }
        #endregion

        public ObservableCollection<object> OpenedDocuments { get; } = new ObservableCollection<Object>();

        #region OpenTournamentCommand
        private RelayCommand _openTournamentCommand;
        public RelayCommand OpenTournamentCommand
        {
            get
            {
                if (_openTournamentCommand == null)
                {
                    _openTournamentCommand = new RelayCommand(() =>
                    {
                    },
                    true);
                }
                return _openTournamentCommand;
            }
        }
        #endregion

        #region CurrentDocuments
        private object _currentDocument;
        public object CurrentDocument
        {
            get
            {
                return _currentDocument;
            }
            set
            {
                if ((_currentDocument == null && value == null) || (_currentDocument != null && _currentDocument.Equals(value)))
                {
                    //equal
                    return;
                }
                _currentDocument = value;
                RaisePropertyChanged(() => CurrentDocument);
                RaisePropertyChanged(() => TournamentRibbonVisibility);
            }
        }

        #endregion

        #region NewTournamentCommand
        private RelayCommand _newTournamentCommand;
        public RelayCommand NewTournamentCommand
        {
            get
            {
                if (_newTournamentCommand == null)
                {
                    _newTournamentCommand = new RelayCommand(async () =>
                    {
                        try
                        {
                            var newTournamentModel = 
                            new Model.Models.Tournament(
                                -1, 
                                "Neues Turnier", 
                                "Beschreibung ergänzen", 
                                DateTime.Now.AddDays(5), 
                                60, 
                                16, 
                                0, 
                                new Model.Models.PlayArea[0], 
                                new Model.Models.Team[0], 
                                null);
                            var res = await App.RestClient.NewTournamentWithHttpMessagesAsync(newTournamentModel);
                            var tournament = ViewModelLocator.Instance.GetTournamentViewModel(res.Body);
                            Tournaments.Add(tournament);
                            OpenedDocuments.Add(tournament);
                            CurrentDocument = tournament;
                        }
                        catch (Microsoft.Rest.HttpOperationException)
                        {
                            MessengerInstance.Send(new CommunicationErrorMessage());
                        }
                    });                    
                }
                return _newTournamentCommand;
            }
        }
        #endregion

        #region DeleteTournamentCommand
        private RelayCommand _deleteTournamentCommand;
        public RelayCommand DeleteTournamentCommand
        {
            get
            {
                if (_deleteTournamentCommand == null)
                {
                    _deleteTournamentCommand = new RelayCommand(() =>
                    {
                        MessengerInstance.Send(new AreYouSureMessage("Turnier löschen", "Das Turnier sowie alle Ergebnisse werden unwiederbringlich gelöscht.\nSind Sie sicher?", () =>
                        {
                            try
                            {
                                SelectedTournament.Close(false);
                                App.RestClient.DeleteTournamentWithHttpMessagesAsync(SelectedTournament.Id);
                                Tournaments.Remove(SelectedTournament);
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
                        }));
                    });
                }
                return _deleteTournamentCommand;
            }
        }
        #endregion

        #region SelectedTournament
        private TournamentViewModel _selectedTournament;
        public TournamentViewModel SelectedTournament
        {
            get
            {
                return _selectedTournament;
            }
            set
            {
                if ((_selectedTournament == null && value == null) || (_selectedTournament != null && value != null && _selectedTournament.Id == value.Id))
                {
                    //Nothing changed
                    return;
                }
                _selectedTournament = value;    
                RaisePropertyChanged(() => SelectedTournament);
            }
        }
        #endregion

        #region TournamentRibbonVisibility
        public Visibility TournamentRibbonVisibility
        {
            get
            {
                if (CurrentDocument is TournamentViewModel)
                {
                    return Visibility.Visible;
                }
                return Visibility.Hidden;
            }
        }
        #endregion

        private Model.Models.Tournament CreateDesignTimeVm()
        {
            #region Dummy PlayAreas
            var playAreas = new List<Model.Models.PlayArea>();
            playAreas.Add(new Model.Models.PlayArea(1, "Area 1", "Play Area 1"));
            playAreas.Add(new Model.Models.PlayArea(1, "Area 2", "Play Area 2"));
            #endregion

            #region Dummy Teams
            var teams = new List<Model.Models.Team>();
            teams.Add(new Model.Models.Team(1, "Team 1"));
            teams.Add(new Model.Models.Team(2, "Team 2"));
            teams.Add(new Model.Models.Team(3, "Team 3"));
            teams.Add(new Model.Models.Team(4, "Team 4"));
            teams.Add(new Model.Models.Team(5, "Team 5"));
            teams.Add(new Model.Models.Team(6, "Team 6"));
            teams.Add(new Model.Models.Team(7, "Team 7"));
            teams.Add(new Model.Models.Team(8, "Team 8"));
            #endregion
            
            var finalMatch = new Model.Models.Match(1, new Model.Models.Team(1, "Team 1"), new Model.Models.Team(2, "Team 2"), playAreas[0], DateTime.Now, DateTime.Now.AddMinutes(60), null, null, 2, null, null, null, null);
            var tournament = new Model.Models.Tournament(1, "Ping-Pong", "nur die Harten kommen in den Garten...", DateTime.Now, 60, 8, 1, playAreas, teams, finalMatch);
            return tournament;
        }
    }
}