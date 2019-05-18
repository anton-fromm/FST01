using FST.TournamentPlanner.API.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.API.Contracts
{
    public interface ITournament
    {

        #region Master data stuff
        /// <summary>
        /// Name of the tournament
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Description of the tournament
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Start day and time of the tournament
        /// 
        /// Only valid while the tournament is in Created-State<see cref="TournamentState"/>
        /// </summary>
        DateTime StartTime { get; set; }

        /// <summary>
        /// Duration a single match within the tournament
        /// 
        /// Only valid while the tournament is in Created-State<see cref="TournamentState"/>
        /// </summary>
        int MatchDuration { get; set; }

        /// <summary>
        /// Number of teams in the tournament.
        /// This number must be expressable as potence of basis 2
        /// 
        /// Only valid while the tournament is in Created-State<see cref="TournamentState"/>
        /// </summary>
        int TeamCount { get; set; }
        #endregion

        #region Tournament state management
        /// <summary>
        /// Current state of the tournament
        /// </summary>
        TournamentState TournamentState { get; }

        /// <summary>
        /// Starts the tournament
        /// 
        /// Only valid while the tournament is in Created-State<see cref="TournamentState"/>
        /// </summary>
        void Start();

        /// <summary>
        /// Ends the tournament
        /// 
        /// Only valid while the tournament is in Started-State<see cref="TournamentState"/>
        /// </summary>
        void End();
        #endregion

        #region PlayArea stuff
        /// <summary>
        /// List of play areas within this tournament
        /// </summary>
        ICollection<IPlayArea> PlayAreas { get; }

        /// <summary>
        /// Adds a new play area to this tournament
        /// 
        /// Only valid while the tournament is in Created-State<see cref="TournamentState"/>
        /// </summary>
        /// <param name="name">name of the play area</param>
        /// <param name="description">description of the play area</param>
        IPlayArea AddPlayArea(string name, string description);

        /// <summary>
        /// Adds a new play area to this tournament
        /// 
        /// Only valid while the tournament is in Created-State<see cref="TournamentState"/>
        /// </summary>
        /// <param name="playArea">play area to add</param>
        IPlayArea AddPlayArea(IPlayArea playArea);

        /// <summary>
        /// Remove the given play area from the tournament
        /// 
        /// Only valid while the tournament is in Created-State<see cref="TournamentState"/>
        /// </summary>
        /// <param name="playArea">play area to remove</param>
        void RemovePlayArea(IPlayArea playArea);
        #endregion

        #region Team stuff
        /// <summary>
        /// Get the list of team within the tournament
        /// </summary>
        ICollection<ITeam> Teams { get; }

        /// <summary>
        /// Adds a new team to this tournament
        /// 
        /// Only valid while the tournament is in Created-State<see cref="TournamentState"/>
        /// </summary>
        /// <param name="name">team to add</param>
        ITeam AddTeam(string name);

        /// <summary>
        /// Adds a new team to this tournament
        /// 
        /// Only valid while the tournament is in Created-State<see cref="TournamentState"/>
        /// </summary>
        /// <param name="name">team to add</param>
        ITeam AddTeam(ITeam team);

        /// <summary>
        /// Removes the given team from the tournament
        /// 
        /// Only valid while the tournament is in Created-State<see cref="TournamentState"/>
        /// </summary>
        /// <param name="name">team to remove</param>
        void RemoveTeam(ITeam team);

        /// <summary>
        /// Removes the given team from the tournament
        /// 
        /// Only valid while the tournament is in Created-State<see cref="TournamentState"/>
        /// </summary>
        /// <param name="name">if of the team to remove</param>
        void RemoveTeam(int id);

        #endregion

        #region Matches
        /// <summary>
        /// Get the matches within the tournament as non-hierachic list
        /// 
        /// Only valid if the tournament is not in Created-State<see cref="TournamentState"/>
        /// </summary>
        /// <returns>matches</returns>
        ICollection<IMatch> GetMatches();

        /// <summary>
        /// Gets the root match element for this tournament (final match).
        /// 
        /// Only valid if the tournament is not in Created-State<see cref="TournamentState"/>
        /// </summary>
        /// <returns></returns>
        IMatch GetRootMatch();
        #endregion
    }
}
