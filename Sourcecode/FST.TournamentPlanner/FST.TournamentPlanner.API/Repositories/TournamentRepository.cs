using FST.TournamentPlanner.DB.Contexts;
using FST.TournamentPlanner.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FST.TournamentPlanner.API.Repositories
{
    public class TournamentRepository : Repository<Tournament>, IRepository<Tournament>
    {
        public TournamentRepository(PlannerContext context) : base(context)
        {

        }
        public override IEnumerable<Tournament> GetAll()
        {
            return PlannerContext.Set<Tournament>().Include(p => p.Teams).Include(p => p.PlayAreas);
        }
    }
}
