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
        ActionResult<Match> EndMatch(int tournamentId, int matchId);
        IActionResult Start(int tournamentId);
        ActionResult<Match> GetMatch(int tournamentId, int matchId);
        ActionResult<Models.PlayArea> AddPlayArea(int tournamentId, string name, string description);
        ActionResult<PlayArea> GetPlayArea(int tournamentId, int playAreaId);
        IActionResult RemovePlayArea(int tournamentId, int playAreaId);
        IActionResult UpdatePlayArea(int tournamentId, Models.PlayArea playAreaId);
        ActionResult<Team> UpdateTeam(int tournamentId, Team team);
        ActionResult RemoveTeam(int tournamentId, int teamId);
        ActionResult<Team> GetTeam(int tournamentId, int teamId);
        ActionResult<Team> AddTeam(int tournamentId, string name);
    }
}
