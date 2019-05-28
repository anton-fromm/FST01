using System.Collections.Generic;
using FST.TournamentPlanner.API.Services;
using FST.TournamentPlanner.DB.Models;
using Microsoft.AspNetCore.Mvc;

namespace FST.TournamentPlanner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private ITournamentService _service;

        public TournamentController(ITournamentService service)
        {
            this._service = service;
        }

        // GET: api/User
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

        [HttpGet]
        public IEnumerable<Models.Tournament> Get()
        {
            return _service.GetAll();
        }

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

        [HttpPost("Start/{tournamentId}")]
        public IActionResult Start(int tournamentId)
        {
            return this._service.Start(tournamentId);
        }
    }
}
