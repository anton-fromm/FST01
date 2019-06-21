using FST.TournamentPlanner.DB.Contexts;
using FST.TournamentPlanner.DB.Models;

namespace FST.TournamentPlanner.API.Repositories
{
    /// <summary>
    /// Data Access for PlayAreaBooking
    /// </summary>
    public class PlayAreaBookingRepository : Repository<PlayAreaBooking>, IRepository<PlayAreaBooking>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayAreaBookingRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PlayAreaBookingRepository(PlannerContext context) : base(context)
        {

        }
    }
}
