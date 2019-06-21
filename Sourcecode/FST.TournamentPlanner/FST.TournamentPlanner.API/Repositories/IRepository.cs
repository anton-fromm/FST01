using FST.TournamentPlanner.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.API.Repositories
{
    /// <summary>
    /// Interface for data manipulation
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Create(TEntity entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Delete(int id);

        /// <summary>
        /// Edits the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Edit(TEntity entity);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Gets the by identifier.
        // read side (could be in separate Readonly Generic Repository)
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        TEntity GetById(int id);

        /// <summary>
        /// Filters this instance.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> Filter();

        /// <summary>
        /// Filters the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Filter(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Saves the changes.
        // separate method SaveChanges can be helpful when using this pattern with UnitOfWork
        /// </summary>
        void SaveChanges();
    }
}
