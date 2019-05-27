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

        public override Tournament GetById(int id)
        {
            return PlannerContext.Set<Tournament>()
                .Include(p => p.Teams)
                .Include(p => p.PlayAreas)
                .Include(p => p.Matches).ThenInclude(p => p.TeamOne)
                .Include(p => p.Matches).ThenInclude(p => p.TeamTwo)
                .Include(p => p.Matches).ThenInclude(p => p.PlayAreaBooking)

                .FirstOrDefault(e => e.Id == id);
        }
        public override IEnumerable<Tournament> GetAll()
        {
            return PlannerContext.Set<Tournament>()
                .Include(p => p.Teams)
                .Include(p => p.PlayAreas)
                .Include(p => p.Matches).ThenInclude(p => p.TeamOne)
                .Include(p => p.Matches).ThenInclude(p => p.TeamTwo)
                .Include(p => p.Matches).ThenInclude(p => p.PlayAreaBooking);
        }
              
    }
}
