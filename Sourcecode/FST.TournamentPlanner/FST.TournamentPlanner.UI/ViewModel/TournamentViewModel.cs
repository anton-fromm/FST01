using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.UI.ViewModel
{
    /// <summary>
    /// Viewmodel representing a tournament
    /// </summary>
    public class TournamentViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TournamentViewModel()
        {
            _tournamentId = 5;
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
                return _tournamentId;
            }
        }
        #endregion

        #region TournamentSpaceCount
        private int _tournamentSpaceCount;
        /// <summary>
        /// Number of available tournament spaces available for the tournament
        /// </summary>
        public int TournamentSpaceCount
        {
            get
            {
                return _tournamentSpaceCount;
            }
            set
            {
                if (_tournamentSpaceCount == value)
                {
                    return;
                }
                _tournamentSpaceCount = value;
                RaisePropertyChanged(() => TournamentSpaceCount);
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
