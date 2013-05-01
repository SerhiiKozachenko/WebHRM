using System.Collections.Generic;
using System.Web.Mvc;

namespace KendoWrapper.Grid.Context
{
    public class GridContextModelBinder : IModelBinder
    {
        #region Implementation of IModelBinder

        /// <summary>
        /// Binds the model to a value by using the specified controller context and binding context.
        /// </summary>
        /// <returns>
        /// The bound value.
        /// </returns>
        /// <param name="controllerContext">The controller context.</param><param name="bindingContext">The binding context.</param>
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = new GridContext
                            {
                                Take = int.Parse(controllerContext.HttpContext.Request["take"]),
                                Skip = int.Parse(controllerContext.HttpContext.Request["skip"]),
                                Page = int.Parse(controllerContext.HttpContext.Request["page"]),
                                PageSize = int.Parse(controllerContext.HttpContext.Request["pageSize"]),
                                SortOrder = controllerContext.HttpContext.Request["sort[0][dir]"] == "asc" ? SortOrder.Asc : SortOrder.Desc,
                                SortColumn = controllerContext.HttpContext.Request["sort[0][field]"],
                                FilterLogic = controllerContext.HttpContext.Request["filter[logic]"] == "or" ? FilterLogic.Or : FilterLogic.And
                            };

            //build filter objects
            var filters = new List<FilterSet>();
            int x = 0;
            while (x < 20)
            {
                var logic = controllerContext.HttpContext.Request["filter[filters][" + x + "][logic]"];

                FilterSet filterSet = null;
                
                if (logic == null)
                {
                    var field = controllerContext.HttpContext.Request["filter[filters][" + x + "][field]"];
                    if (field == null)
                    {
                        break;
                    }

                    var val = controllerContext.HttpContext.Request["filter[filters][" + x + "][value]"] ?? string.Empty;
                    var strop = controllerContext.HttpContext.Request["filter[filters][" + x + "][operator]"];

                    filterSet = new FilterSet
                                        {
                                            Filter1 = new FilterInfo
                                                          {
                                                              Operator = FilterInfo.ParseOperator(strop),
                                                              Field = field,
                                                              Value = val
                                                          }
                                        };
                }
                else
                {
                    var field1 = controllerContext.HttpContext.Request["filter[filters][" + x + "][filters][0][field]"];
                    var oper1 = controllerContext.HttpContext.Request["filter[filters][" + x + "][filters][0][operator]"];
                    var val1 = controllerContext.HttpContext.Request["filter[filters][" + x + "][filters][0][value]"];

                    var field2 = controllerContext.HttpContext.Request["filter[filters][" + x + "][filters][1][field]"];
                    var oper2 = controllerContext.HttpContext.Request["filter[filters][" + x + "][filters][1][operator]"];
                    var val2 = controllerContext.HttpContext.Request["filter[filters][" + x + "][filters][1][value]"];

                    filterSet = new FilterSet
                                    {
                                        Filter1 = new FilterInfo
                                                        {
                                                            Field = field1,
                                                            Operator = FilterInfo.ParseOperator(oper1),
                                                            Value = val1
                                                        },
                                        Filter2 = new FilterInfo
                                                        {
                                                            Field = field2,
                                                            Operator = FilterInfo.ParseOperator(oper2),
                                                            Value = val2
                                                        },
                                        Logic = FilterSet.ParseLogic(logic)
                                    };
                }

                filters.Add(filterSet);

                x++;
            }

            model.Filters = filters;

            return model;
        }

        #endregion
    }
}