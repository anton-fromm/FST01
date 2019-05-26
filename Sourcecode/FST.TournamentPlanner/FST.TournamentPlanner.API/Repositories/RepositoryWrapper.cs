﻿using FST.TournamentPlanner.DB.Contexts;
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

        public RepositoryWrapper(PlannerContext context)
        {
            _ctx = context;
        }
    }
}
