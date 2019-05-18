using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.API.Contracts
{
    /// <summary>
    /// Match interface class
    /// 
    /// Represents a single match within a tournament
    /// </summary>
    public interface IMatch
    {
        /// <summary>
        /// First team in the match
        /// </summary>
        ITeam TeamOne { get; }

        /// <summary>
        /// Second team in the match
        /// </summary>
        ITeam TeamTwo { get; }

        /// <summary>
        /// Play area this match is held on
        /// </summary>
        IPlayArea PlayArea { get; }

        /// <summary>
        /// Start time of the match
        /// </summary>
        DateTime Start { get; }
        
        /// <summary>
        /// End time of the match
        /// </summary>
        DateTime End { get; }

        /// <summary>
        /// Score of team one
        /// </summary>
        int? TeamOneScore { get; }

        /// <summary>
        /// Score of team two
        /// </summary>
        int? TeamTwoScore { get; }

        /// <summary>
        /// Set the match result
        /// </summary>
        /// <param name="scoreTeamOne">score of team one</param>
        /// <param name="scoreTeamTwo">score of team two</param>
        void SetScore(int scoreTeamOne, int scoreTeamTwo);

        /// <summary>
        /// Winner of the match
        /// </summary>
        /// <returns>Winner team</returns>
        ITeam GetWinner();

        /// <summary>
        /// Looser of the match
        /// </summary>
        /// <returns>Looser team</returns>
        ITeam GetLooser();
    }
}
