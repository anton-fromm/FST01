using FST.TournamentPlanner.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.API.Services
{
    /// <summary>
    /// Interface for Business Logic
    /// </summary>
    public interface ITournamentService
    {
        /// <summary>Gets the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Tournament Get(int id);

        /// <summary>Gets all.</summary>
        /// <returns></returns>
        IEnumerable<Tournament> GetAll();

        /// <summary>
        /// Sets the score.
        /// </summary>
        /// <param name="matchId">The match identifier.</param>
        /// <param name="scoreOne">The score one.</param>
        /// <param name="scoreTwo">The score two.</param>
        /// <returns></returns>
        IActionResult SetScore(int matchId,int scoreOne, int scoreTwo);

        /// <summary>
        /// Ends the match.
        /// </summary>
        /// <param name="tournamentId">The tournament identifier.</param>
        /// <param name="matchId">The match identifier.</param>
        /// <returns></returns>
        ActionResult<Match> EndMatch(int tournamentId, int matchId);

        /// <summary>
        /// Starts the specified tournament identifier.
        /// </summary>
        /// <param name="tournamentId">The tournament identifier.</param>
        /// <returns></returns>
        IActionResult Start(int tournamentId);

        /// <summary>
        /// Gets the match.
        /// </summary>
        /// <param name="tournamentId">The tournament identifier.</param>
        /// <param name="matchId">The match identifier.</param>
        /// <returns></returns>
        ActionResult<Match> GetMatch(int tournamentId, int matchId);

        /// <summary>
        /// Adds the play area.
        /// </summary>
        /// <param name="tournamentId">The tournament identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        ActionResult<PlayArea> AddPlayArea(int tournamentId, string name, string description);

        /// <summary>
        /// Gets the play area.
        /// </summary>
        /// <param name="tournamentId">The tournament identifier.</param>
        /// <param name="playAreaId">The play area identifier.</param>
        /// <returns></returns>
        ActionResult<PlayArea> GetPlayArea(int tournamentId, int playAreaId);

        /// <summary>
        /// Removes the play area.
        /// </summary>
        /// <param name="tournamentId">The tournament identifier.</param>
        /// <param name="playAreaId">The play area identifier.</param>
        /// <returns></returns>
        IActionResult RemovePlayArea(int tournamentId, int playAreaId);

        /// <summary>
        /// Updates the play area.
        /// </summary>
        /// <param name="tournamentId">The tournament identifier.</param>
        /// <param name="playAreaId1">The play area id1.</param>
        /// <param name="playAreaId">The play area identifier.</param>
        /// <returns></returns>
        IActionResult UpdatePlayArea(int tournamentId, int playAreaId1, Models.PlayArea playAreaId);

        /// <summary>
        /// Updates the team.
        /// </summary>
        /// <param name="tournamentId">The tournament identifier.</param>
        /// <param name="teamId">The team identifier.</param>
        /// <param name="team">The team.</param>
        /// <returns></returns>
        ActionResult<Team> UpdateTeam(int tournamentId, int teamId, Team team);

        /// <summary>
        /// Adds the tournament.
        /// </summary>
        /// <param name="tournament">The tournament.</param>
        /// <returns></returns>
        ActionResult<Tournament> AddTournament(Tournament tournament);

        /// <summary>
        /// Removes the team.
        /// </summary>
        /// <param name="tournamentId">The tournament identifier.</param>
        /// <param name="teamId">The team identifier.</param>
        /// <returns></returns>
        ActionResult RemoveTeam(int tournamentId, int teamId);

        /// <summary>
        /// Gets the team.
        /// </summary>
        /// <param name="tournamentId">The tournament identifier.</param>
        /// <param name="teamId">The team identifier.</param>
        /// <returns></returns>
        ActionResult<Team> GetTeam(int tournamentId, int teamId);

        /// <summary>
        /// Adds the team.
        /// </summary>
        /// <param name="tournamentId">The tournament identifier.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        ActionResult<Team> AddTeam(int tournamentId, string name);

        /// <summary>
        /// Removes the tournament.
        /// </summary>
        /// <param name="tournamentId">The tournament identifier.</param>
        /// <returns></returns>
        IActionResult RemoveTournament(int tournamentId);

        /// <summary>
        /// Updates the tournament.
        /// </summary>
        /// <param name="tournamentId">The tournament identifier.</param>
        /// <param name="tournament">The tournament.</param>
        /// <returns></returns>
        IActionResult UpdateTournament(int tournamentId, Tournament tournament);
    }
}
