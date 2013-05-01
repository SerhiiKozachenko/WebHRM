using System;
using System.Linq;
using System.Linq.Expressions;

namespace KendoWrapper.Grid.Filtering.Filters
{
    public class GreaterOrEqualsFilter<T> : BaseFilter<T>
    {
        #region Overrides of BaseFilter<T>

        public override IQueryable<T> Filter(string field, string value, IQueryable<T> query)
        {
            var memberExpression = Expression.PropertyOrField(Expression.Parameter(typeof(T), "expr"), field);
            BinaryExpression binaryExpression;
            if (typeof(T).GetProperty(field).PropertyType == typeof(DateTime))
            {
                var searchExpression = Expression.Constant(DateTime.Parse(value), typeof(DateTime));
                binaryExpression = Expression.GreaterThanOrEqual(Expression.PropertyOrField(memberExpression, "Date"), searchExpression);
            }
            else
            {
                var type = typeof(T).GetProperty(field).PropertyType;
                var searchExpression = Expression.Convert(Expression.Constant(double.Parse(value), typeof(double)), type);
                binaryExpression = Expression.GreaterThanOrEqual(memberExpression, searchExpression);
            }

            var lambda = Expression.Lambda<Func<T, bool>>(binaryExpression, new[] { base.GetParameterExpression(memberExpression.Expression) });

            return query.Where(lambda);
        }

        #endregion
    }
}