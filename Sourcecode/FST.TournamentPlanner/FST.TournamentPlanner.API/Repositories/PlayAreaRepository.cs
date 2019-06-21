using FST.TournamentPlanner.DB.Contexts;
using FST.TournamentPlanner.DB.Models;

namespace FST.TournamentPlanner.API.Repositories
{
    /// <summary>
    /// Data Access for PlayArea
    /// </summary>
    public class PlayAreaRepository : Repository<PlayArea>, IRepository<PlayArea>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayAreaRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PlayAreaRepository(PlannerContext context) : base(context)
        {

        }
    }
}
