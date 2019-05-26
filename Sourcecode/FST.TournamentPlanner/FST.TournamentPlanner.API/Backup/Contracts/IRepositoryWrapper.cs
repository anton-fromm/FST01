﻿using FST.TournamentPlanner.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.API.Contracts
{
    public interface IRepositoryWrapper
    {
        TournamentRepository Tournament { get; }
    }
}
