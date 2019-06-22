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
                    //Update current match, since one of the previous matches is finished now
                    Model = App.RestClient.GetMatchWithHttpMessagesAsync(Model.Id.Value, _tournament.Id.Value).Result.Body;
                    UpdateValuesFromModel();
                }
            });

            // Init values from model object
            UpdateValuesFromModel();
        }
        #endregion

        private void UpdateValuesFromModel()
        {
            _teamOneScore = Model.TeamOneScore;
            RaisePropertyChanged(() => TeamOneScore);
            _teamTwoScore = Model.TeamTwoScore;
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
                return Model.Id.Value;
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
                if (Model.FirstPredecessor == null)
                {
                    return null;
                }
                if (_firstPredecessor == null)
                {
                    _firstPredecessor = ViewModelLocator.Instance.GetMatchViewModel(_tournament, Model.FirstPredecessor, this);
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
                if (Model.SecondPredecessor == null)
                {
                    return null;
                }
                if (_secondPredecessor == null)
                {
                    _secondPredecessor = ViewModelLocator.Instance.GetMatchViewModel(_tournament, Model.SecondPredecessor, this);
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
                return ViewModelLocator.Instance.GetTeamViewModel(_tournament, Model.TeamOne);
            }
        }

        #endregion

        #region TeamTwo

        public TeamViewModel TeamTwo
        {
            get
            {
                return ViewModelLocator.Instance.GetTeamViewModel(_tournament, Model.TeamTwo);
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
                return Model.MatchState.Value;
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
        public PlayAreaViewModel PlayArea => ViewModelLocator.Instance.GetPlayAreaViewModel(_tournament, Model.PlayArea);
        #endregion

        #region StartTime
        public TimeSpan StartTime => Model.Start.Value.TimeOfDay;
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
                    () => ScoreIsEditable && TeamOneScore.HasValue);
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
                    () =>
                    {
                        try
                        {
                            CurrentlyFinishing = true;
                            var res = App.RestClient.EndMatchWithHttpMessagesAsync(_tournament.Id.Value, Id);
                            Model = res.Result.Body;
                            UpdateValuesFromModel();
                            if (Successor != null)
                            {
                                // Inform successor about finished prematch
                                Successor.UpdateMatch();
                            }
                            else
                            {
                                MessengerInstance.Send(new TournamentFinishedMessage(_tournament.Id.Value));
                            }
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

        private void UpdateMatch()
        {
            try
            {
                Model = App.RestClient.GetMatchWithHttpMessagesAsync(Id, _tournament.Id.Value).Result.Body;
                UpdateValuesFromModel();
            }
            catch (Microsoft.Rest.HttpOperationException)
            {
                MessengerInstance.Send(new CommunicationErrorMessage());
                CurrentlyFinishing = false;
            }
        }
        #endregion
    }
}
