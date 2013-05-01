using System;
using System.Linq;
using System.Linq.Expressions;

namespace KendoWrapper.Grid.Filtering.Filters
{
    public class EqualFilter<T> : BaseFilter<T>
    {
        #region Implementation of IFilterable<T>

        public override IQueryable<T> Filter(string field, string value, IQueryable<T> query)
        {
            MemberExpression memberExpression;
            BinaryExpression binaryExpression;
            // CASE: FOREIGN KEY - TYPE
            if (field.ToLower().EndsWith("id"))
            {
                var typeName = field.ToLower().Replace("id", string.Empty);
                memberExpression = Expression.PropertyOrField(Expression.PropertyOrField(Expression.Parameter(typeof(T), "expr"), typeName), "Id");

                var searchExpression = Expression.Constant(long.Parse(value), typeof(long));
                binaryExpression = Expression.Equal(memberExpression, searchExpression);

                var fklambda = Expression.Lambda<Func<T, bool>>(binaryExpression, new[] { base.GetParameterExpression(memberExpression.Expression) });

                return query.Where(fklambda);
            }

            memberExpression = Expression.PropertyOrField(Expression.Parameter(typeof(T), "expr"), field);

            if (typeof(T).GetProperty(field).PropertyType == typeof(DateTime))
            {
                var searchExpression = Expression.Constant(DateTime.Parse(value), typeof(DateTime));
                binaryExpression = Expression.Equal(Expression.PropertyOrField(memberExpression, "Date"), searchExpression);
            }
            else if (typeof(T).GetProperty(field).PropertyType == typeof(string))
            {
                var searchExpression = Expression.Constant(value, typeof(string));
                binaryExpression = Expression.Equal(memberExpression, searchExpression);
            }
            else
            {
                var type = typeof(T).GetProperty(field).PropertyType;
                var searchExpression = Expression.Convert(Expression.Constant(double.Parse(value), typeof(double)), type);
                binaryExpression = Expression.Equal(memberExpression, searchExpression);
            }

            var lambda = Expression.Lambda<Func<T, bool>>(binaryExpression, new[] { base.GetParameterExpression(memberExpression.Expression) });
            
            return query.Where(lambda);
        }

        #endregion
    }
}