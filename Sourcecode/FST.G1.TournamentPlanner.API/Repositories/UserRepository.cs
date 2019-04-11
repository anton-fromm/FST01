using FST.G1.TournamentPlanner.API.Contracts;
using FST.G1.TournamentPlanner.DB.Contexts;
using FST.G1.TournamentPlanner.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FST.G1.TournamentPlanner.API.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(PlannerContext context) : base(context)
        {
        }
    }
}
