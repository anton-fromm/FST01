using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FST.G1.TournamentPlanner.API.Contracts
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
    }
}
