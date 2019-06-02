using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// Constructor
        /// </summary>
        public TournamentViewModel(Model.Models.Tournament tournament) : base(tournament)
        {
            tournament.PlayAreas.ToList().ForEach(p => PlayAreas.Add(ViewModelLocator.Instance.GetPlayAreaViewModel(p)));
            _name = tournament.Name;
            _startDate = tournament.StartTime ?? DateTime.Now;
            _description = tournament.Description;
            _maximumMatchDururationInMinutes = tournament.MatchDuration ?? 30;
            _teamCount = _model.TeamCount ?? 16;

        }

        #region Teams
        public ObservableCollection<TeamViewModel> Teams
        {
            get
            {
                return new ObservableCollection<TeamViewModel>(_model.Teams.Select(t => ViewModelLocator.Instance.GetTeamViewModel(t)));
            }
        }
        #endregion

        #region MasterData

        #region TournamentId
        /// <summary>
        /// Id of the tournament
        /// </summary>
        public Int32 TournamentId
        {
            get
            {
                return _model.Id.Value;
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
                    MessengerInstance.Send(new Messages.TournamentAllreadyStarted());
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
                    MessengerInstance.Send(new Messages.TournamentAllreadyStarted());
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
                    MessengerInstance.Send(new Messages.TournamentAllreadyStarted());
                }
                else
                {
                    if (_startDate.TimeOfDay != value.TimeOfDay)
                    {
                        _startDate = _startDate.Add(value.TimeOfDay);
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
                    MessengerInstance.Send(new Messages.TournamentAllreadyStarted());
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
                    MessengerInstance.Send(new Messages.TournamentAllreadyStarted());
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
                    MessengerInstance.Send(new Messages.TournamentAllreadyStarted());
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
                return _model.State.Value;
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
        private MatchViewModel _finalMatch;
        public List<MatchViewModel> FinalMatch
        {
            get
            {
                if (_finalMatch == null)
                {
                    _finalMatch = ViewModelLocator.Instance.GetMatchViewModel(_model.FinalMatch, null);
                }
                return new List<MatchViewModel>() { _finalMatch };
            }
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
                    _closeCommand = new RelayCommand(() => { }, CanClose);
                }
                return _closeCommand;
            }
        }
        #endregion

        #endregion

        #region HasChanges
        public bool HasChanges
        {
            get
            {
                return
                    _model.Name != Name ||
                    _model.Description != Description ||
                    _model.TeamCount != TeamCount ||
                    _model.StartTime != _startDate ||
                    _model.MatchDuration != _maximumMatchDururationInMinutes;
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
                        // PlayArea-Count != 0
                        // TeamCount == Teams.Count
                        // Neues Modell holen
                        // Alle Properties => RaisePropertyChanged / Command.CanExecute updaten

                    }, State == STATE_CREATED);
                }
                return StartCommand;
            }
        }
        #endregion
    }
}
