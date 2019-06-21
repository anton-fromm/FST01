using FST.TournamentPlanner.DB.Contexts;
using FST.TournamentPlanner.DB.Models;

namespace FST.TournamentPlanner.API.Repositories
{
    /// <summary>
    /// Data Access for Team
    /// </summary>
    public class TeamRepository : Repository<Team>, IRepository<Team>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TeamRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public TeamRepository(PlannerContext context) : base(context)
        {

        }
    }
}
