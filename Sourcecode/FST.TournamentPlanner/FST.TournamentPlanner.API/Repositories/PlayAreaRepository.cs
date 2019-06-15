using FST.TournamentPlanner.DB.Contexts;
using FST.TournamentPlanner.DB.Models;

namespace FST.TournamentPlanner.API.Repositories
{
    public class PlayAreaRepository : Repository<PlayArea>, IRepository<PlayArea>
    {
        public PlayAreaRepository(PlannerContext context) : base(context)
        {

        }
    }
}
