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
        [HttpGet]
        public Model.Tournament Get()
        {
            Model.Tournament x =  this._service.GenerateMatchPlan();
            return x;
        }
    }
}
