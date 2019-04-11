using FST.G1.TournamentPlanner.API.Repositories;
using FST.G1.TournamentPlanner.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FST.G1.TournamentPlanner.API.Contracts
{
    public interface IUserRepository : IRepository<User>
    {
    }
}
