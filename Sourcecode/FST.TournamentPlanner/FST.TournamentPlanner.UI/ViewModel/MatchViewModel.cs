using FST.TournamentPlanner.UI.ViewModel.Messages;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.UI.ViewModel
{
    public class MatchViewModel : ViewModelBase<Model.Models.Match>
    {
        public const int STATE_CREATED = 0;
        public const int STATE_STARTED = 1;
        public const int STATE_FINISHED = 2;

        private Model.Models.Tournament _tournament;

        #region Constructors
        public MatchViewModel(Model.Models.Tournament tournament, Model.Models.Match match) : this(tournament, match, null)
        {
        }

        public MatchViewModel(Model.Models.Tournament tournament, Model.Models.Match match, MatchViewModel successor) : base(match)
        {
            if (tournament == null)
            {
                throw new NullReferenceException();
            }
            _tournament = tournament;
            Successor = successor;

            // Register message type to get informed about changes in predeseccors
            MessengerInstance.Register<MatchFinishedMessage>(this, m =>
            {
                if (m.TournamentId == tournament.Id && m.Match == FirstPredecessor || m.Match == SecondPredecessor)
                {
                    //TODO: Update current match, since one of the previous matches is finished now
                    // Get fresh version of the match from Rest API
                }
            });

            // Init values from model object
            UpdateValuesFromModel();
        }
        #endregion

        private void UpdateValuesFromModel()
        {
            _teamOneScore = _model.TeamOneScore;
            RaisePropertyChanged(() => TeamOneScore);
            _teamTwoScore = _model.TeamTwoScore;
            RaisePropertyChanged(() => TeamTwoScore);
            RaisePropertyChanged(() => TeamOne);
            RaisePropertyChanged(() => TeamTwo);
            RaisePropertyChanged(() => Winner);
            RaisePropertyChanged(() => State);
            RaisePropertyChanged(() => ScoreIsEditable);
            FinishCommand.RaiseCanExecuteChanged();
        }

        #region Id
        public int Id
        {
            get
            {
                return _model.Id.Value;
            }
        }
        #endregion

        #region Successor
        public MatchViewModel Successor { get; }
        #endregion

        #region Predecessors
        public List<MatchViewModel> Predecessors
        {
            get
            {
                var res = new List<MatchViewModel>();
                if (FirstPredecessor == null)
                {
                    return res;
                }
                return new List<MatchViewModel>() { FirstPredecessor, SecondPredecessor };
            }
    }
        #endregion 

        #region FirstPredecessor
        private MatchViewModel _firstPredecessor;
        public MatchViewModel FirstPredecessor
        {
            get
            {
                if (_model.FirstPredecessor == null)
                {
                    return null;
                }
                if (_firstPredecessor == null)
                {
                    _firstPredecessor = ViewModelLocator.Instance.GetMatchViewModel(_tournament, _model.FirstPredecessor, this);
                }
                return _firstPredecessor;
            }
        }
        #endregion

        #region SecondPredecessor
        private MatchViewModel _secondPredecessor;
        public MatchViewModel SecondPredecessor
        {
            get
            {
                if (_model.SecondPredecessor == null)
                {
                    return null;
                }
                if (_secondPredecessor == null)
                {
                    _secondPredecessor = ViewModelLocator.Instance.GetMatchViewModel(_tournament, _model.SecondPredecessor, this);
                }
                return _secondPredecessor;
            }
        }
        #endregion

        #region TeamOne

        public TeamViewModel TeamOne
        {
            get
            {
                return ViewModelLocator.Instance.GetTeamViewModel(_tournament, _model.TeamOne);
            }
        }

        #endregion

        #region TeamTwo

        public TeamViewModel TeamTwo
        {
            get
            {
                return ViewModelLocator.Instance.GetTeamViewModel(_tournament, _model.TeamTwo);
            }
        }

        #endregion

        #region TeamOneScore
        private int? _teamOneScore;
        public int? TeamOneScore
        {
            get
            {
                return _teamOneScore;
            }
            set
            {
                if (ScoreIsEditable)
                {
                    _teamOneScore = value;
                    if (TeamTwoScore == null)
                    {
                        // In case this is the first time a result is entered, initialize the other score with 0
                        TeamTwoScore = 0;
                    }
                    App.RestClient.SetScoreOnMatchWithHttpMessagesAsync(this.Id, TeamOneScore.Value, TeamTwoScore.Value);
                    RaisePropertyChanged(() => TeamOneScore);
                }
            }
        }
        #endregion

        #region TeamTwoScore
        private int? _teamTwoScore;
        public int? TeamTwoScore
        {
            get
            {
                return _teamTwoScore;
            }
            set
            {
                if (ScoreIsEditable)
                {
                    _teamTwoScore = value;
                    if (TeamOneScore == null)
                    {
                        // In case this is the first time a result is entered, initialize the other score with 0
                        TeamOneScore = 0;
                    }
                    App.RestClient.SetScoreOnMatchWithHttpMessagesAsync(this.Id, TeamOneScore.Value, TeamTwoScore.Value);
                    RaisePropertyChanged(() => TeamTwoScore);
                }
            }
        }

        #endregion

        #region Winner
        public TeamViewModel Winner
        {
            get
            {
                if (State != STATE_FINISHED)
                {
                    return null;
                }
                if (TeamOneScore > TeamTwoScore)
                {
                    return TeamOne;
                }
                return TeamTwo;
            }
        }
        #endregion

        #region Looser
        public TeamViewModel Looser
        {
            get
            {
                if (State != STATE_FINISHED)
                {
                    return null;
                }
                if (TeamOneScore < TeamTwoScore)
                {
                    return TeamOne;
                }
                return TeamTwo;
            }
        }
        #endregion

        #region State
        public int State
        {
            get
            {
                return _model.MatchState.Value;
            }
        }
        #endregion

        #region ScoreIsEditable
        public bool ScoreIsEditable
        {
            get
            {
                return TeamOne != null && TeamTwo != null && State != STATE_FINISHED && !CurrentlyFinishing;
            }
        }
        #endregion

        #region PlayArea
        public PlayAreaViewModel PlayArea => ViewModelLocator.Instance.GetPlayAreaViewModel(_tournament, _model.PlayArea);
        #endregion

        #region StartTime
        public TimeSpan StartTime => _model.Start.Value.TimeOfDay;
        #endregion

        #region CurrentlyFinishing
        private bool _currentlyFinishing = false;
        protected bool CurrentlyFinishing
        {
            get
            {
                return _currentlyFinishing;
            }
            set
            {
                if (_currentlyFinishing == value)
                {
                    return;
                }
                _currentlyFinishing = value;
                RaisePropertyChanged(() => ScoreIsEditable);
                RaisePropertyChanged(() => CurrentlyFinishing);
                FinishCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region FinishCommand
        private RelayCommand _finishCommand;
        public RelayCommand FinishCommand
        {
            get
            {
                if (_finishCommand == null)
                {
                    _finishCommand = new RelayCommand(() =>
                    {
                        Finish();
                    },
                    () => ScoreIsEditable);
                }
                return _finishCommand;
            }
        }

        private void Finish()
        {
            MessengerInstance.Send(
                new AreYouSureMessage(
                    "Spiel abschließen",
                    "Änderungen an dem Ergebnis sind nach Abschluss nicht mehr möglich.\nSind Sie sicher, dass Sie das Spiel beenden wollen?",
                    async () =>
                    {
                        try
                        {
                            CurrentlyFinishing = true;
                            var res = await App.RestClient.EndMatchWithHttpMessagesAsync(_tournament.Id.Value, Id);
                            _model = res.Body;
                            UpdateValuesFromModel();
                            // Inform successor about finished prematch
                            MessengerInstance.Send(new MatchFinishedMessage(_tournament.Id.Value, this));
                            CurrentlyFinishing = false;
                        }
                        catch (Microsoft.Rest.HttpOperationException)
                        {
                            MessengerInstance.Send(new CommunicationErrorMessage());
                            CurrentlyFinishing = false;
                        }
                    }));
        }
        #endregion
    }
}
