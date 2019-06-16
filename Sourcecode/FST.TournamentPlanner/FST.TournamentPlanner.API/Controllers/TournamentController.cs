using System.Collections.Generic;
using System.Threading.Tasks;
using FST.TournamentPlanner.API.Services;
using FST.TournamentPlanner.DB.Models;
using Microsoft.AspNetCore.Mvc;

namespace FST.TournamentPlanner.API.Controllers
{
    /// <summary>
    /// This class 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private ITournamentService _service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        public TournamentController(ITournamentService service)
        {
            this._service = service;
        }

        #region Tournament CRUD

        /// <summary>
        /// Create a new tournament
        /// </summary>
        /// <returns></returns>
        [HttpGet("New")]
        public Task<ActionResult<Models.Tournament>> NewTournament()
        {
            return null;
        }

        /// <summary>
        /// Get the tournament by its Id
        /// </summary>
        /// <param name="id">Id of the tournament</param>
        /// <returns>tournament object</returns>
        [HttpGet("{id}")]
        public Models.Tournament Get(int id)
        {
            Models.Tournament x = this._service.Get(id);
            if (x == null)
            {
                return null;
            }
            return x;
        }

        /// <summary>
        /// Delete the given tournament by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Delete/{id}")]
        public IActionResult DeleteTournament(int id)
        {
            return null;
        }

        /// <summary>
        /// Get the list of all tournaments
        /// </summary>
        /// <returns>tournaments</returns>
        [HttpGet]
        public IEnumerable<Models.Tournament> GetAll()
        {
            return _service.GetAll();
        }

        /// <summary>
        /// Update the tournament´s master data
        /// </summary>
        /// <param name="tournament">tournament to update</param>
        /// <returns>updated tournament</returns>
        [HttpPost("Update")]
        public Task<ActionResult<Models.Tournament>> UpdateTournament(Models.Tournament tournament)
        {
            return null;
        }
        #endregion

        /// <summary>
        /// Set the score for a match. This call is only valid if the match is not finished yet
        /// </summary>
        /// <param name="matchId">Id of the match</param>
        /// <param name="scoreOne">current score of team one</param>
        /// <param name="scoreTwo">current score of team two</param>
        /// <returns>Resultcode</returns>
        [HttpPost("Match/{matchId}/{scoreOne}/{scoreTwo}")]
        public IActionResult SetScoreOnMatch(int matchId, int scoreOne, int scoreTwo)
        {
            #region inputValidation
            if (matchId <= 0)
            {
                return new BadRequestObjectResult(matchId);
            }

            if (scoreOne < 0)
            {
                return new BadRequestObjectResult(scoreOne);
            }

            if (scoreTwo < 0)
            {
                return new BadRequestObjectResult(scoreTwo);
            }

            #endregion inputValidation

            return this._service.SetScore(matchId, scoreOne, scoreTwo);
        }

        /// <summary>
        /// end a match
        /// </summary>
        /// <param name="tournamentId"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        [HttpPost("{tournamentId}/Match/{matchId}/End")]
        public ActionResult<Models.Match> EndMatch(int tournamentId, int matchId)
        {
            return _service.EndMatch(tournamentId, matchId);
        }

        /// <summary>
        /// Get a match
        /// </summary>
        /// <param name="tournamentId"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        [HttpPost("Match/{matchId}")]
        public ActionResult<Models.Match> GetMatch(int tournamentId, int matchId)
        {
            return _service.GetMatch(tournamentId, matchId);
        }

        /// <summary>
        /// Starts the tournament. 
        /// This call is only valid if the tournament is in state "Created"
        /// </summary>
        /// <param name="tournamentId"></param>
        /// <returns></returns>
        [HttpPost("Start/{tournamentId}")]
        public IActionResult Start(int tournamentId)
        {
            return this._service.Start(tournamentId);
        }

        #region Play area CRUD

        /// <summary>
        /// create a play area
        /// </summary>
        /// <param name="tournamentId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        [HttpPost("{tournamentId}/PlayArea/Add/{name}/{description}")]
        public ActionResult<Models.PlayArea> AddPlayArea(int tournamentId, string name, string description)
        {
            return this._service.AddPlayArea(tournamentId, name, description);
        }

        /// <summary>
        /// get a play area
        /// </summary>
        /// <param name="tournamentId"></param>
        /// <param name="playAreaId"></param>
        /// <returns></returns>
        [HttpGet("{tournamentId}/PlayArea/{playAreaId}")]
        public ActionResult<Models.PlayArea> GetPlayArea(int tournamentId, int playAreaId)
        {
            return this._service.GetPlayArea(tournamentId, playAreaId);
        }

        /// <summary>
        /// remove a play area
        /// </summary>
        /// <param name="tournamentId"></param>
        /// <param name="playAreaId"></param>
        /// <returns></returns>
        [HttpPost("{tournamentId}/PlayArea/{playAreaId}/Remove")]
        public IActionResult RemovePlayArea(int tournamentId, int playAreaId)
        {
            return this._service.RemovePlayArea(tournamentId, playAreaId);
        }

        /// <summary>
        /// update play area
        /// </summary>
        /// <param name="tournamentId"></param>
        /// <param name="playAreaId"></param>
        /// <param name="playArea"></param>
        /// <returns></returns>
        [HttpPost("{tournamentId}/PlayArea/{playAreaId}")]
        public IActionResult UpdatePlayArea(int tournamentId, int playAreaId, Models.PlayArea playArea)
        {
            return this._service.UpdatePlayArea(tournamentId, playAreaId, playArea);
        }

        #endregion

        #region CRUDTeam

        /// <summary>
        /// Add a new team
        /// </summary>
        /// <param name="tournamentId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost("{tournamentId}/Team/Add/{name}")]
        public ActionResult<Models.Team> AddTeam(int tournamentId, string name)
        {
            return this._service.AddTeam(tournamentId, name);
        }

        /// <summary>
        /// Get a team
        /// </summary>
        /// <param name="tournamentId"></param>
        /// <param name="teamId"></param>
        /// <returns></returns>
        [HttpPost("{tournamentId}/Team/{teamId}")]
        public ActionResult<Models.Team> GetTeam(int tournamentId, int teamId)
        {
            return this._service.GetTeam(tournamentId, teamId);
        }

        /// <summary>
        /// Remove the team from the tournament
        /// </summary>
        /// <param name="tournamentId">Id of the tournament</param>
        /// <param name="teamId">Id of the team</param>
        /// <returns></returns>
        [HttpPost("{tournamentId}/Team/{teamId}/Remove")]
        public ActionResult RemoveTeam(int tournamentId, int teamId)
        {
            return this._service.RemoveTeam(tournamentId, teamId);
        }

        /// <summary>
        /// Update an existing team
        /// </summary>
        /// <param name="tournamentId"></param>        
        /// <param name="teamId">Id of the team</param>
        /// <param name="team"></param>
        /// <returns></returns>
        [HttpPost("{tournamentId}/Team/Update")]
        public ActionResult<Models.Team> UpdateTeam(int tournamentId, int teamId, Models.Team team)
        {
            return this._service.UpdateTeam(tournamentId, teamId, team);
        }
        #endregion
    }
}
