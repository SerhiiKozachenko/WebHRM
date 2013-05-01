using System;
using System.Linq;
using System.Linq.Expressions;

namespace KendoWrapper.Grid.Filtering.Filters
{
    public class StartsWithFilter<T> : BaseFilter<T>
    {
        #region Overrides of BaseFilter<T>

        public override IQueryable<T> Filter(string field, string value, IQueryable<T> query)
        {
            if (typeof(T).GetProperty(field).PropertyType == typeof(DateTime))
            {
                return query;
            }

            var memberExpression = Expression.PropertyOrField(Expression.Parameter(typeof(T), "expr"), field);
            var method = typeof(String).GetMethod("StartsWith", new[] { typeof(String) });
            var startsWith = Expression.Call(memberExpression, method, Expression.Constant(value));
            var lambda = Expression.Lambda<Func<T, bool>>(startsWith, new[] { base.GetParameterExpression(memberExpression.Expression) });
            
            return query.Where(lambda);
        }

        #endregion
    }
}