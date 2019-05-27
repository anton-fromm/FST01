using FST.TournamentPlanner.DB.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.API.Repositories
{
    public class RepositoryWrapper: IRepositoryWrapper
    {
        private PlannerContext _ctx;
        private TournamentRepository _tournament;
        private MatchRepository _match;



        public TournamentRepository Tournament
        {
            get
            {
                if (_tournament == null)
                {
                    _tournament = new TournamentRepository(_ctx);
                }

                return _tournament;
            }
        }



        public MatchRepository Match
        {
            get
            {
                if (_match == null)
                {
                    _match = new MatchRepository(_ctx);
                }

                return _match;
            }
        }

        public RepositoryWrapper(PlannerContext context)
        {
            _ctx = context;
        }
    }
}
