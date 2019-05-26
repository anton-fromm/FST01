using FST.TournamentPlanner.API.Models;
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
        Tournament GenerateMatchPlan();
    }
}
