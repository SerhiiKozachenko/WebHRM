using System.Linq;
using System.Linq.Expressions;
using KendoWrapper.Grid.Filtering.Interfaces;

namespace KendoWrapper.Grid.Filtering.Filters
{
    public abstract class BaseFilter<T> : IFilterable<T>
    {
        #region Implementation of IFilterable<T>

        public abstract IQueryable<T> Filter(string field, string value, IQueryable<T> query);
      
        #endregion

        protected ParameterExpression GetParameterExpression(Expression expression)
        {
            while (expression.NodeType == ExpressionType.MemberAccess)
            {
                expression = ((MemberExpression)expression).Expression;
            }

            if (expression.NodeType == ExpressionType.Parameter)
            {
                return (ParameterExpression)expression;
            }

            return null;
        }
    }
}