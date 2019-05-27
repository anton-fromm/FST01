using FST.TournamentPlanner.DB.Contexts;
using FST.TournamentPlanner.DB.Models;

namespace FST.TournamentPlanner.API.Repositories
{
    public class PlayAreaBookingRepository : Repository<PlayAreaBooking>, IRepository<PlayAreaBooking>
    {
        public PlayAreaBookingRepository(PlannerContext context) : base(context)
        {

        }
    }
}
