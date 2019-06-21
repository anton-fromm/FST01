using FST.TournamentPlanner.DB.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.API.Repositories
{
    /// <summary>
    /// Wrapper implementation
    /// </summary>
    /// <seealso cref="FST.TournamentPlanner.API.Repositories.IRepositoryWrapper" />
    public class RepositoryWrapper: IRepositoryWrapper
    {
        private readonly PlannerContext _ctx;
        private TournamentRepository _tournament;
        private MatchRepository _match;
        private PlayAreaBookingRepository _playAreaBooking;
        private PlayAreaRepository _playArea;
        private TeamRepository _team;

        /// <summary>
        /// Gets the tournament.
        /// </summary>
        /// <value>
        /// The tournament.
        /// </value>
        public TournamentRepository Tournament
        {
            get
            {
                if (this._tournament == null)
                {
                    this._tournament = new TournamentRepository(this._ctx);
                }

                return this._tournament;
            }
        }



        /// <summary>
        /// Gets the match.
        /// </summary>
        /// <value>
        /// The match.
        /// </value>
        public MatchRepository Match
        {
            get
            {
                if (this._match == null)
                {
                    this._match = new MatchRepository(this._ctx);
                }

                return this._match;
            }
        }

        /// <summary>
        /// Gets the play area booking.
        /// </summary>
        /// <value>
        /// The play area booking.
        /// </value>
        public PlayAreaBookingRepository PlayAreaBooking
        {
            get
            {
                if (this._playAreaBooking == null)
                {
                    this._playAreaBooking = new PlayAreaBookingRepository(this._ctx);
                }
                return this._playAreaBooking;
            }
        }

        /// <summary>
        /// Gets the play area.
        /// </summary>
        /// <value>
        /// The play area.
        /// </value>
        public PlayAreaRepository PlayArea
        {
            get
            {
                if (this._playArea == null)
                {
                    this._playArea = new PlayAreaRepository(this._ctx);
                }
                return this._playArea;
            }
        }

        /// <summary>
        /// Gets the team.
        /// </summary>
        /// <value>
        /// The team.
        /// </value>
        public TeamRepository Team
        {
            get
            {
                if (this._team == null)
                {
                    this._team = new TeamRepository(this._ctx);
                }
                return this._team;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryWrapper"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public RepositoryWrapper(PlannerContext context)
        {
            this._ctx = context;
        }
    }
}
