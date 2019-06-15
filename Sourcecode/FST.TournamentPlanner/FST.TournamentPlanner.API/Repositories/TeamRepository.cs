using FST.TournamentPlanner.DB.Contexts;
using FST.TournamentPlanner.DB.Models;

namespace FST.TournamentPlanner.API.Repositories
{
    public class TeamRepository : Repository<Team>, IRepository<Team>
    {
        public TeamRepository(PlannerContext context) : base(context)
        {

        }
    }
}
