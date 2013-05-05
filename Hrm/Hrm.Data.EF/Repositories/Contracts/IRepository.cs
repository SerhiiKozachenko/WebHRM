using System.Linq;
using Hrm.Data.EF.Specifications.Contracts;

namespace Hrm.Data.EF.Repositories.Contracts
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

        void SaveChanges();

        IQueryable<TEntity> Find(ISpecification<TEntity> specification);

        TEntity FindOne(ISpecification<TEntity> specification);

        IOrderedQueryable<TEntity> SortByAsc(string propertyName, IQueryable<TEntity> data = null);

        IOrderedQueryable<TEntity> SortByDesc(string propertyName, IQueryable<TEntity> data = null);
    }
}