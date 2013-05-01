using System.Linq;
using KendoWrapper.Grid.Context;
using KendoWrapper.Grid.Filtering.Filters;
using KendoWrapper.Grid.Filtering.Interfaces;

namespace KendoWrapper.Grid.Filtering
{
    public class GridFilter<T>
    {
        private readonly IFilterable<T> containsFilter;

        private readonly IFilterable<T> notContainsFilter;

        private readonly IFilterable<T> startsWithFilter;

        private readonly IFilterable<T> endsWithFilter;

        private readonly IFilterable<T> equalFilter;

        private readonly IFilterable<T> notEqualFilter;

        private readonly IFilterable<T> greaterFilter;

        private readonly IFilterable<T> greaterOrEqualsFilter;

        private readonly IFilterable<T> lessFilter;

        private readonly IFilterable<T> lessOrEqualFilter;

        public GridFilter()
        {
            this.containsFilter = new ContainsFilter<T>();
            this.notContainsFilter = new NotContainsFilter<T>();
            this.startsWithFilter = new StartsWithFilter<T>();
            this.endsWithFilter = new EndsWithFilter<T>();
            this.equalFilter = new EqualFilter<T>();
            this.notEqualFilter = new NotEqualFilter<T>();
            this.greaterFilter = new GreaterFilter<T>();
            this.greaterOrEqualsFilter = new GreaterOrEqualsFilter<T>();
            this.lessFilter = new LessThanFilter<T>();
            this.lessOrEqualFilter = new LessThanOrEqualsFilter<T>();
        }

        public IQueryable<T> Filter (string field, string value, FilterOperations filter, IQueryable<T> query)
        {
            switch (filter)
            {
                    case FilterOperations.Contains:
                        return this.containsFilter.Filter(field, value, query);

                    case FilterOperations.NotContains:
                        return this.notContainsFilter.Filter(field, value, query);

                    case FilterOperations.StartsWith:
                        return this.startsWithFilter.Filter(field, value, query);

                    case FilterOperations.EndsWith:
                        return this.endsWithFilter.Filter(field, value, query);

                    case FilterOperations.Equals:
                        return this.equalFilter.Filter(field, value, query);

                    case FilterOperations.NotEquals:
                        return this.notEqualFilter.Filter(field, value, query);

                    case FilterOperations.Greater:
                        return this.greaterFilter.Filter(field, value, query);

                    case FilterOperations.GreaterOrEquals:
                        return this.greaterOrEqualsFilter.Filter(field, value, query);

                    case FilterOperations.LessThan:
                        return this.lessFilter.Filter(field, value, query);

                    case FilterOperations.LessThanOrEquals:
                        return this.lessOrEqualFilter.Filter(field, value, query);

                    default:
                        return null;
            }
        }
    }
}