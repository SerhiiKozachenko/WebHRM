using System;
using System.Linq.Expressions;

namespace Hrm.Data.EF.Specifications.Contracts
{
    public interface ISpecification<TEntity>
    {
        Expression<Func<TEntity, bool>> IsSatisfiedBy();
    }
}