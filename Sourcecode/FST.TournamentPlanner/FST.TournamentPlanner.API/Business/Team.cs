﻿using FST.TournamentPlanner.API.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.API.Business
{
    public class Team : ITeam
    {
        private Tournament _tournament;
        protected Team(Tournament tournament)
        {
            _tournament = tournament;
        }
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
