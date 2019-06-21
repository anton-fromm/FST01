using FST.TournamentPlanner.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.API.Repositories
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        void Create(TEntity entity);
        void Delete(TEntity entity);
        void Delete(int id);
        void Edit(TEntity entity);
        IEnumerable<TEntity> GetAll(); 

        //read side (could be in separate Readonly Generic Repository)
        TEntity GetById(int id);
        IEnumerable<TEntity> Filter();
        IEnumerable<TEntity> Filter(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);

        //separate method SaveChanges can be helpful when using this pattern with UnitOfWork
        void SaveChanges();
    }
}
