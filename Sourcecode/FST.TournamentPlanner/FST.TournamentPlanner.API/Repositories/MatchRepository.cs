using FST.TournamentPlanner.DB.Contexts;
using FST.TournamentPlanner.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FST.TournamentPlanner.API.Repositories
{
    public class MatchRepository : Repository<Match>, IRepository<Match>
    {
        public MatchRepository(PlannerContext context) : base(context)
        {
        }

        public override Match GetById(int id)
        {
            return PlannerContext.Set<Match>()
                .Include(p => p.TeamOne).ThenInclude(t => t.Team)
                .Include(p => p.TeamTwo).ThenInclude(t => t.Team)
                .Include(p => p.Successor).ThenInclude(p => p.TeamOne).ThenInclude(p => p.Team)
                .Include(p => p.Successor).ThenInclude(p => p.TeamTwo).ThenInclude(p => p.Team)
                .Include(p => p.PlayAreaBooking)

                .FirstOrDefault(e => e.Id == id);
        }
        public override IEnumerable<Match> GetAll()
        {
            return PlannerContext.Set<Match>()
                .Include(p => p.TeamOne).ThenInclude(t => t.Team)
                .Include(p => p.TeamTwo).ThenInclude(t => t.Team)
                .Include(p => p.Successor).ThenInclude(p => p.TeamOne).ThenInclude(p => p.Team)
                .Include(p => p.Successor).ThenInclude(p => p.TeamTwo).ThenInclude(p => p.Team)
                .Include(p => p.PlayAreaBooking);
        }
    }
}
    

