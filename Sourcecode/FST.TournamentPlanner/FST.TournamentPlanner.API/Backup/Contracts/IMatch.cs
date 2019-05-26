using FST.TournamentPlanner.API.Business;
using FST.TournamentPlanner.DB.Models;
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
        /// Id of the match
        /// </summary>
        int Id { get; }

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
        DateTime StartTime { get; }
        
        /// <summary>
        /// End time of the match
        /// </summary>
        DateTime EndTime { get; }

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
        ///  
        /// Only valid while the match is in state <see cref="MatchState">Started</see>
        /// </summary>
        /// <param name="scoreTeamOne">score of team one</param>
        /// <param name="scoreTeamTwo">score of team two</param>
        void SetScore(int scoreTeamOne, int scoreTeamTwo);

        /// <summary>
        /// State of the current match
        /// </summary>
        MatchState MatchState { get; }
        /// <summary>
        /// Start the match
        /// 
        /// Only valid while the match is in state <see cref="MatchState">Planned</see>
        /// </summary>
        void Start();

        /// <summary>
        /// Start the match
        /// 
        /// Only valid while the match is in state <see cref="MatchState">Started</see>
        void End();

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

        /// <summary>
        /// Follow-up match, where the winner compeeds
        /// </summary>
        IMatch Successor { get; }

        /// <summary>
        /// Previous match, which TeamOne won, to enter the current match
        /// </summary>
        IMatch FirstPredecessor { get; }

        /// <summary>
        /// Previous match, which TeamTwo won, to enter the current match
        /// </summary>
        IMatch SecondPredecessor { get; }

    }
}
