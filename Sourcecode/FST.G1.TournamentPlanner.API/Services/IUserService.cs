﻿using FST.G1.TournamentPlanner.DB.Models;
using System.Collections.Generic;

namespace FST.G1.TournamentPlanner.API.Services
{
    public interface IUserService
    {
        int Create(User user);
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Update(int id, User user);
        void Delete(int id);
    }
}
