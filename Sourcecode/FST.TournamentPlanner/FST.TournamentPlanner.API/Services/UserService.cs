using FST.TournamentPlanner.API.Contracts;
using FST.TournamentPlanner.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.API.Services
{
    public class UserService : IUserService
    {
        private IRepositoryWrapper _repoWrapper;

        public UserService(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        public int Create(User user)
        {
            User userEntity = new User
            {
                Name = user.Name
            };

            _repoWrapper.User.Create(userEntity);
            _repoWrapper.User.SaveChanges();

            return userEntity.Id;
        }

        public void Delete(int id)
        {
            this._repoWrapper.User.Delete(id);
            this._repoWrapper.User.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return this._repoWrapper.User.Filter();
        }

        public User GetById(int id)
        {
            return this._repoWrapper.User.GetById(id);
        }

        public void Update(int id, User user)
        {
            User dbUser = this._repoWrapper.User.GetById(id);
            dbUser.Name = user.Name;
            this._repoWrapper.User.SaveChanges();
        }
    }
}
