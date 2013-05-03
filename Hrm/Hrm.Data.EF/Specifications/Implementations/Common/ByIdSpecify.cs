using System;
using System.Linq.Expressions;
using Hrm.Data.EF.Models.Base;
using Hrm.Data.EF.Specifications.Contracts;

namespace Hrm.Data.EF.Specifications.Implementations.Common
{
    public class ByIdSpecify<TEntity> : ISpecification<TEntity> where TEntity : BaseModel<long>
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