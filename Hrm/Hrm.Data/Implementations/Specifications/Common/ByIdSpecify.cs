using System;
using System.Linq.Expressions;
using Hrm.Core.Entities.Base;
using Hrm.Core.Interfaces.Specifications.Base;

namespace Hrm.Data.Implementations.Specifications.Common
{
    public class ByIdSpecify<TEntity> : ISpecification<TEntity> where TEntity : BaseEntity
    {
        private readonly long id;

        public ByIdSpecify(long id)
        {
            this.id = id;
        }

        #region Implementation of ISpecification<TEntity>

        public Expression<Func<TEntity, bool>> IsSatisfiedBy()
        {
            return x => x.Id.Equals(this.id);
        }

        #endregion
    }
}