using FST.G1.TournamentPlanner.API.Contracts;
using FST.G1.TournamentPlanner.DB.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FST.G1.TournamentPlanner.API.Repositories
{
    public class RepositoryWrapper: IRepositoryWrapper
    {
        private PlannerContext _ctx;
        private IUserRepository _user;


        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_ctx);
                }

                return _user;
            }
        }

        public RepositoryWrapper(PlannerContext context)
        {
            _ctx = context;
        }
    }
}
