using FST.TournamentPlanner.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.API.Services
{
    public interface ITournamentService
    {
        Tournament GenerateMatchPlan();
    }
}
