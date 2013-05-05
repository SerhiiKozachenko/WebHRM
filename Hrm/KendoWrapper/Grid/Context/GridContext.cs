using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KendoWrapper.Grid.Filtering;

namespace KendoWrapper.Grid.Context
{
    [ModelBinder(typeof(GridContextModelBinder))]
    public class GridContext
    {
        public int Take { get; set; }

        public int Skip { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public bool HasSorting
        {
            get { return !string.IsNullOrEmpty(this.SortColumn); }
        }

        public SortOrder SortOrder { get; set; }

        public string SortColumn { get; set; }

        public bool HasFilters
        {
            get { return this.Filters.Any(); }
        }

        public FilterLogic FilterLogic { get; set; }

        public IList<FilterSet> Filters { get; set; }

        public GridContext()
        {
            this.Filters = new List<FilterSet>();
        }

        public IQueryable<T> ApplyFilters<T>(IQueryable<T> query)
        {
            if (this.HasFilters)
            {
                var gridFilter = new GridFilter<T>();
                IQueryable<T> filteredQuery = null;

                foreach (var filterSet in this.Filters)
                {
                    if (filterSet.Filter1 != null)
                    {
                        switch (FilterLogic)
                        {
                                case FilterLogic.And:
                                    filteredQuery = gridFilter.Filter(filterSet.Filter1.Field, filterSet.Filter1.Value,
                                                           filterSet.Filter1.Operator, filteredQuery ?? query);
                                break;

                                case FilterLogic.Or:
                                    if (filteredQuery != null)
                                    {
                                        filteredQuery =
                                            filteredQuery.AsParallel().ToList().Union(gridFilter.Filter(filterSet.Filter1.Field,
                                                                              filterSet.Filter1.Value,
                                                                              filterSet.Filter1.Operator,
                                                                              query).AsParallel().ToList()).AsQueryable();
                                    }
                                    else
                                    {
                                        filteredQuery = gridFilter.Filter(filterSet.Filter1.Field, filterSet.Filter1.Value,
                                                           filterSet.Filter1.Operator, query);
                                    }
                                break;
                        }
                        
                    }

                    if (filterSet.Filter2 != null)
                    {
                        switch (filterSet.Logic)
                        {
                            case FilterLogic.And:
                                filteredQuery = gridFilter.Filter(filterSet.Filter2.Field, filterSet.Filter2.Value,
                                                           filterSet.Filter2.Operator, filteredQuery ?? query);
                                break;

                            case FilterLogic.Or:
                                filteredQuery =
                                    filteredQuery.AsParallel().ToList().Union(gridFilter.Filter(filterSet.Filter2.Field,
                                                                              filterSet.Filter2.Value,
                                                                              filterSet.Filter2.Operator,
                                                                              query).AsParallel().ToList()).AsQueryable();
                                break;
                        }
                    }
                }

                return filteredQuery;
            }

            return null;
        }
    }
}