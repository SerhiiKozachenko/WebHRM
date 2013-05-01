using System;
using System.Linq.Expressions;

namespace Hrm.Core.Interfaces.Specifications.Base
{
    public interface ISpecification<TEntity>
    {
        Expression<Func<TEntity, bool>> IsSatisfiedBy();
    }
}