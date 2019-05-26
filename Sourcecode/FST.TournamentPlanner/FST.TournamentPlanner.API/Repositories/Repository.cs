using FST.TournamentPlanner.DB.Contexts;
using FST.TournamentPlanner.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FST.TournamentPlanner.API.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
            where TEntity : class, IEntity
    {
        protected PlannerContext PlannerContext { get; set; }

        public Repository(PlannerContext context)
        {
            PlannerContext = context;
        }
        public void Create(TEntity entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            PlannerContext.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            PlannerContext.Set<TEntity>().Remove(entity);
        }

        public void Delete(int id)
        {
            var entityToDelete = PlannerContext.Set<TEntity>().FirstOrDefault(e => e.Id == id);
            if (entityToDelete != null)
            {
                PlannerContext.Set<TEntity>().Remove(entityToDelete);
            }
        }

        public void Edit(TEntity entity)
        {
            var editedEntity = PlannerContext.Set<TEntity>().FirstOrDefault(e => e.Id == entity.Id);
            editedEntity = entity;
        }

        public TEntity GetById(int id)
        {
            return PlannerContext.Set<TEntity>().FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<TEntity> Filter()
        {
            return PlannerContext.Set<TEntity>();
        }

        public IEnumerable<TEntity> Filter(Func<TEntity, bool> predicate)
        {
            return PlannerContext.Set<TEntity>().Where(predicate);
        }

        public void SaveChanges() => PlannerContext.SaveChanges();

        public virtual IEnumerable<TEntity> GetAll()
        {
            return PlannerContext.Set<TEntity>();
        }
    }
}
