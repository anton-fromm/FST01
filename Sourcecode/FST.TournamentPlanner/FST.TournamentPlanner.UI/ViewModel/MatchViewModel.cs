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

        public MatchViewModel(Model.Models.Match match) : this(match, null)
        {
            // Register message type to get informed about changes in predeseccors
            MessengerInstance.Register<MatchFinishedMessage>(this, m =>
            {
                if (m.Match == FirstPredecessor || m.Match == SecondPredecessor)
                {
                    //TODO: Update current match, since one of the previous matches is finished now
                    // Get fresh version of the match from Rest API
                }
            });
        }

        public int Id
        {
            get
            {
                return _model.Id.Value;
            }
        }

        public MatchViewModel(Model.Models.Match match, MatchViewModel successor) : base(match) => Successor = successor;

        #region Successor
        public MatchViewModel Successor { get; }
        #endregion

        #region Predecessors
        public List<MatchViewModel> Predecessors
        {
            get
            {
                if (FirstPredecessor == null)
                {
                    return null;
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
                    _firstPredecessor = ViewModelLocator.Instance.GetMatchViewModel(_model.FirstPredecessor, this);
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
                    _secondPredecessor = ViewModelLocator.Instance.GetMatchViewModel(_model.SecondPredecessor, this);
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
                return ViewModelLocator.Instance.GetTeamViewModel(_model.TeamOne);
            }
        }

        #endregion

        #region TeamTwo

        public TeamViewModel TeamTwo
        {
            get
            {
                return ViewModelLocator.Instance.GetTeamViewModel(_model.TeamTwo);
            }
        }

        #endregion

        #region TeamOneScore
        private int? _teamOneScore;

        public int? TeamOneScore
        {
            get
            {
                return _model.TeamOneScore;
            }
            set
            {
                _teamOneScore = value;
            }
        }
        #endregion

        #region TeamTwoScore
        private int? _teamTwoScore;

        public int? TeamTwoScore
        {
            get
            {
                return _model.TeamTwoScore;
            }
            set
            {
                _teamTwoScore = value;
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

        #region State
        public int State
        {
            get
            {
                return _model.MatchState.Value;
            }
        }
        #endregion

        private RelayCommand _finishCommand;
        public RelayCommand FinishCommand
        {
            get
            {
                if (_finishCommand == null)
                {
                    _finishCommand = new RelayCommand(() =>
                    {
                        // ToDO
                        MessengerInstance.Send(new MatchFinishedMessage(this));
                    },
                    () => State != STATE_FINISHED);
                }
                return _finishCommand;
            }
        }
    }
}
