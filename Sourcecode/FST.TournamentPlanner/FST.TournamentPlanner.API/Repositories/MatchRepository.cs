using FST.TournamentPlanner.DB.Contexts;
using FST.TournamentPlanner.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FST.TournamentPlanner.API.Repositories
{
    /// <summary>
    /// Data Access for match
    /// </summary>
    public class MatchRepository : Repository<Match>, IRepository<Match>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MatchRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public MatchRepository(PlannerContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public override Match GetById(int id)
        {
            return this.PlannerContext.Set<Match>()
                .Include(p => p.TeamOne).ThenInclude(t => t.Team)
                .Include(p => p.TeamTwo).ThenInclude(t => t.Team)
                .Include(p => p.Successor).ThenInclude(p => p.TeamOne).ThenInclude(p => p.Team)
                .Include(p => p.Successor).ThenInclude(p => p.TeamTwo).ThenInclude(p => p.Team)
                .Include(p => p.PlayAreaBooking)

                .FirstOrDefault(e => e.Id == id);
        }
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Match> GetAll()
        {
            return this.PlannerContext.Set<Match>()
                .Include(p => p.TeamOne).ThenInclude(t => t.Team)
                .Include(p => p.TeamTwo).ThenInclude(t => t.Team)
                .Include(p => p.Successor).ThenInclude(p => p.TeamOne).ThenInclude(p => p.Team)
                .Include(p => p.Successor).ThenInclude(p => p.TeamTwo).ThenInclude(p => p.Team)
                .Include(p => p.PlayAreaBooking);
        }
    }
}
    

