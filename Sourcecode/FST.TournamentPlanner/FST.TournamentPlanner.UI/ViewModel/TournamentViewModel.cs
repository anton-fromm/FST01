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
        /// Constructor
        /// </summary>
        public TournamentViewModel(Model.Models.Tournament tournament) : base(tournament)
        {
        }

        public ObservableCollection<TeamViewModel> Teams
        {
            get
            {
                return new ObservableCollection<TeamViewModel>(_model.Teams.Select(t => new TeamViewModel(t)));
            }
        }

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

        #region TournamentId
        private Int32 _tournamentId;
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

        #region PlayAreaCount
        /// <summary>
        /// Number of available tournament spaces available for the tournament
        /// </summary>
        public int PlayAreaCount
        {
            get
            {
                return _model.PlayAreas.Count();
            }
        }
        #endregion

        #region MaximumMatchDurationInMinutes
        private int _maximumMatchDurationInMinutes;

        public int MaximumMatchDurationInMinutes
        {
            get { return _maximumMatchDurationInMinutes; }
            set { _maximumMatchDurationInMinutes = value; }
        }
        #endregion

        #region TournamentStart
        private DateTime _tournamentStart;

        public DateTime TournamentStart
        {
            get
            {
                return _tournamentStart;
            }
            set
            {
                if (_tournamentStart == null || _tournamentStart.Equals(value))
                {
                    return;
                }
                _tournamentStart = value;
                RaisePropertyChanged(() => TournamentStart);
            }
        }

        #endregion

        public string Name
        {
            get
            {
                return _model.Name;
            }
        }

        #region Description
        private String _description;
        public String Description
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_description))
                {
                    return String.Empty;
                }
                return _description;
            }
            set
            {
                if (_description == null && (_description == null || _description.Equals(value)))
                {
                    return;
                }
                _description = value;
                RaisePropertyChanged(() => Description);
            }
        }
        #endregion

        private TournamentMatchViewModel _finalMatch;
        public List<TournamentMatchViewModel> FinalMatch
        {
            get
            {
                if (_finalMatch == null)
                {
                    _finalMatch = new TournamentMatchViewModel(_model.FinalMatch);
                }
                return new List<TournamentMatchViewModel>() { _finalMatch };
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is TournamentViewModel)
            {
                return ((TournamentViewModel)obj).TournamentId == this.TournamentId;
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return TournamentId;
        }
    }
}
