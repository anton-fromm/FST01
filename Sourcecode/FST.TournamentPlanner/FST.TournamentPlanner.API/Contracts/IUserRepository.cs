using FST.TournamentPlanner.API.Repositories;
using FST.TournamentPlanner.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.API.Contracts
{
    public interface IUserRepository : IRepository<User>
    {
    }
}
