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
        /// <summary>
        /// Get the tournament by its Id
        /// </summary>
        /// <param name="id">Id of the tournament</param>
        /// <returns>tournament object</returns>
        [HttpGet("{id}")]
        public Models.Tournament Get(int id)
        {
            Models.Tournament x =  this._service.Get(id);
            if (x == null)
            {
                return null;
            }
            return x;
        }

        /// <summary>
        /// Get the list of all tournaments
        /// </summary>
        /// <returns>tournaments</returns>
        [HttpGet]
        public IEnumerable<Models.Tournament> Get()
        {
            return _service.GetAll();
        }

        /// <summary>
        /// Set the score for a match. This call is only valid if the match is not finished yet
        /// </summary>
        /// <param name="matchId">Id of the match</param>
        /// <param name="scoreOne">current score of team one</param>
        /// <param name="scoreTwo">current score of team two</param>
        /// <returns>Resultcode</returns>
        [HttpPost]
        public IActionResult SetScoreOnMatch (int matchId, int scoreOne, int scoreTwo)
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

        [HttpPost("{tournamentId}/PlayArea/Add/{name}/{description}")]
        public IActionResult AddPlayArea(int tournamentId, string name, string description)
        {
            return new OkResult();
        }

        [HttpGet("{tournamentId}/PlayArea/Get/{playAreaId}")]
        public Task<ActionResult<Models.PlayArea>> GetPlayArea(int tournamentId, int playAreaId)
        {
            return null;
        }

    }
}
