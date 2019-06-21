using FST.TournamentPlanner.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.API.Repositories
{
    /// <summary>
    /// Wrapper over all available repos
    /// </summary>
    public interface IRepositoryWrapper
    {
        /// <summary>
        /// Gets the tournament.
        /// </summary>
        /// <value>
        /// The tournament.
        /// </value>
        TournamentRepository Tournament { get; }

        /// <summary>
        /// Gets the match.
        /// </summary>
        /// <value>
        /// The match.
        /// </value>
        MatchRepository Match { get; }

        /// <summary>
        /// Gets the play area booking.
        /// </summary>
        /// <value>
        /// The play area booking.
        /// </value>
        PlayAreaBookingRepository PlayAreaBooking { get; }

        /// <summary>
        /// Gets the play area.
        /// </summary>
        /// <value>
        /// The play area.
        /// </value>
        PlayAreaRepository PlayArea { get; }

        /// <summary>
        /// Gets the team.
        /// </summary>
        /// <value>
        /// The team.
        /// </value>
        TeamRepository Team { get; }
    }
}
