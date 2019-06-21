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
    /// Data Access for Tournament
    /// </summary>
    public class TournamentRepository : Repository<Tournament>, IRepository<Tournament>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TournamentRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public TournamentRepository(PlannerContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public override Tournament GetById(int id)
        {
            return this.PlannerContext.Set<Tournament>()
                .Include(p => p.Teams)
                .Include(p => p.PlayAreas)
                .Include(p => p.Matches).ThenInclude(p => p.TeamOne)
                .Include(p => p.Matches).ThenInclude(p => p.TeamTwo)
                .Include(p => p.Matches).ThenInclude(p => p.PlayAreaBooking)

                .FirstOrDefault(e => e.Id == id);
        }
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Tournament> GetAll()
        {
            return this.PlannerContext.Set<Tournament>()
                .Include(p => p.Teams)
                .Include(p => p.PlayAreas)
                .Include(p => p.Matches).ThenInclude(p => p.TeamOne)
                .Include(p => p.Matches).ThenInclude(p => p.TeamTwo)
                .Include(p => p.Matches).ThenInclude(p => p.PlayAreaBooking);
        }
              
    }
}
