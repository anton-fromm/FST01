using FST.TournamentPlanner.DB.Contexts;
using FST.TournamentPlanner.DB.Models;

namespace FST.TournamentPlanner.API.Repositories
{
    public class MatchRepository : Repository<Match>, IRepository<Match>
    {
        public MatchRepository(PlannerContext context) : base(context)
        {

        }
    }
}
