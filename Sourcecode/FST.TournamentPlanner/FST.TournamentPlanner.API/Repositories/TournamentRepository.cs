using FST.TournamentPlanner.API.Contracts;
using FST.TournamentPlanner.DB.Contexts;
using FST.TournamentPlanner.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.API.Repositories
{
    public class TournamentRepository : Repository<Tournament>, IRepository<Tournament>
    {
        public TournamentRepository(PlannerContext context) : base(context)
        {
        }
    }
}
