using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.UI.ViewModel
{
    public class TournamentMatchViewModel : ViewModelBase<Model.Models.Match>
    {
        public TournamentMatchViewModel(Model.Models.Match match) : this(match, null)
        {
        }

        public int Id
        {
            get
            {
                return _model.Id.Value;
            }
        }

        public TournamentMatchViewModel(Model.Models.Match match, TournamentMatchViewModel successor) : base(match) => Successor = successor;

        #region Successor
        public TournamentMatchViewModel Successor { get; }
        #endregion

        #region Predecessors
        public List<TournamentMatchViewModel> Predecessors
        {
            get
            {
                if (FirstPredecessor == null)
                {
                    return null;
                }
                return new List<TournamentMatchViewModel>() { FirstPredecessor, SecondPredecessor };
            }
    }
        #endregion 

        #region FirstPredecessor
        private TournamentMatchViewModel _firstPredecessor;
        public TournamentMatchViewModel FirstPredecessor
        {
            get
            {
                if (_model.FirstPredecessor == null)
                {
                    return null;
                }
                if (_firstPredecessor == null)
                {
                    _firstPredecessor = new TournamentMatchViewModel(_model.FirstPredecessor, this);
                }
                return _firstPredecessor;
            }
        }
        #endregion

        #region SecondPredecessor
        private TournamentMatchViewModel _secondPredecessor;
        public TournamentMatchViewModel SecondPredecessor
        {
            get
            {
                if (_model.SecondPredecessor == null)
                {
                    return null;
                }
                if (_secondPredecessor == null)
                {
                    _secondPredecessor = new TournamentMatchViewModel(_model.SecondPredecessor, this);
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
                return new TeamViewModel(_model.TeamOne);
            }
        }

        #endregion

        #region TeamTwo

        public TeamViewModel TeamTwo
        {
            get
            {
                return new TeamViewModel(_model.TeamTwo);
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
                if (State != 2)
                {
                    return new TeamViewModel(null);
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
    }
}
