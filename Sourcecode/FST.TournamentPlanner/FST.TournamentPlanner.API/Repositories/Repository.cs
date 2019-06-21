using FST.TournamentPlanner.DB.Contexts;
using FST.TournamentPlanner.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FST.TournamentPlanner.API.Repositories
{
    /// <summary>
    /// Generic Repository
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="FST.TournamentPlanner.API.Repositories.IRepository{TEntity}" />
    public class Repository<TEntity> : IRepository<TEntity>
            where TEntity : class, IEntity
    {
        /// <summary>
        /// Gets or sets the planner context.
        /// </summary>
        /// <value>
        /// The planner context.
        /// </value>
        protected PlannerContext PlannerContext { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Repository(PlannerContext context)
        {
            PlannerContext = context;
        }
        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Create(TEntity entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            PlannerContext.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(TEntity entity)
        {
            PlannerContext.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            var entityToDelete = PlannerContext.Set<TEntity>().FirstOrDefault(e => e.Id == id);
            if (entityToDelete != null)
            {
                PlannerContext.Set<TEntity>().Remove(entityToDelete);
            }
        }

        /// <summary>
        /// Edits the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Edit(TEntity entity)
        {
            var editedEntity = PlannerContext.Set<TEntity>().FirstOrDefault(e => e.Id == entity.Id);
            editedEntity = entity;
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual TEntity GetById(int id)
        {
            return PlannerContext.Set<TEntity>().FirstOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Filters this instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> Filter()
        {
            return PlannerContext.Set<TEntity>();
        }

        /// <summary>
        /// Filters the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> Filter(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return PlannerContext.Set<TEntity>().Where(predicate);
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        public void SaveChanges() => PlannerContext.SaveChanges();

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return PlannerContext.Set<TEntity>();
        }
    }
}
