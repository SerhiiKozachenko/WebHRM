using System;
using System.Linq;
using System.Linq.Expressions;

namespace KendoWrapper.Grid.Filtering.Filters
{
    public class ContainsFilter<T> : BaseFilter<T>
    {
        #region Overrides of BaseFilter<T>

        public override IQueryable<T> Filter(string field, string value, IQueryable<T> query)
        {
            if (typeof(T).GetProperty(field).PropertyType == typeof(DateTime))
            {
                return query;
            }

            var memberExpression = Expression.PropertyOrField(Expression.Parameter(typeof(T), "expr"), field);
            var method = typeof(String).GetMethod("Contains", new[] { typeof(String) });
            var contains = Expression.Call(memberExpression, method, Expression.Constant(value));
            var lambda = Expression.Lambda<Func<T, bool>>(contains, new[] { base.GetParameterExpression(memberExpression.Expression) });
            
            return query.Where(lambda);
        }

        #endregion
    }
}