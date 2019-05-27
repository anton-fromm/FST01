using FST.TournamentPlanner.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.API.Services
{
    public interface ITournamentService
    {
        Tournament Get(int id);
        IEnumerable<Tournament> GetAll();
        IActionResult SetScore(int matchId,int scoreOne, int scoreTwo);
        IActionResult Start(int tournamentId);
    }
}
