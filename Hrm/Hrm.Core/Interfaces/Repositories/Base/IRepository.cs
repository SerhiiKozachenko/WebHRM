using System.Linq;
using Hrm.Core.Interfaces.Specifications.Base;

namespace Hrm.Core.Interfaces.Repositories.Base
{
    public interface IRepository<TEntity> : IQueryable<TEntity>
    {
        /// <summary>
        /// Saves the specified entity.
        /// </summary>
        /// <param name="entity">The entity instance.</param>
        void SaveOrUpdate(TEntity entity);

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity instance.</param>
        void Delete(TEntity entity);

        IQueryable<TEntity> Find(ISpecification<TEntity> specification);

        TEntity FindOne(ISpecification<TEntity> specification);

        IOrderedQueryable<TEntity> SortByAsc(string propertyName, IQueryable<TEntity> data = null);

        IOrderedQueryable<TEntity> SortByDesc(string propertyName, IQueryable<TEntity> data = null);
    }
}