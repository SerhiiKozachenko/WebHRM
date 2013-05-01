namespace KendoWrapper.Grid.Context
{
    public class FilterInfo
    {
        public string Field { get; set; }

        public FilterOperations Operator { get; set; }
       
        public string Value { get; set; }

        public static FilterOperations ParseOperator(string theOperator)
        {
            switch (theOperator)
            {
                //equal ==
                case "eq":
                case "==":
                case "isequalto":
                case "equals":
                case "equalto":
                case "equal":
                    return FilterOperations.Equals;
                //not equal !=
                case "neq":
                case "!=":
                case "isnotequalto":
                case "notequals":
                case "notequalto":
                case "notequal":
                case "ne":
                    return FilterOperations.NotEquals;
                // Greater
                case "gt":
                case ">":
                case "isgreaterthan":
                case "greaterthan":
                case "greater":
                    return FilterOperations.Greater;
                // Greater or equal
                case "gte":
                case ">=":
                case "isgreaterthanorequalto":
                case "greaterthanequal":
                case "ge":
                    return FilterOperations.GreaterOrEquals;
                // Less
                case "lt":
                case "<":
                case "islessthan":
                case "lessthan":
                case "less":
                    return FilterOperations.LessThan;
                // Less or equal
                case "lte":
                case "<=":
                case "islessthanorequalto":
                case "lessthanequal":
                case "le":
                    return FilterOperations.LessThanOrEquals;
                case "startswith":
                    return FilterOperations.StartsWith;

                case "endswith":
                    return FilterOperations.EndsWith;
                //string.Contains()
                case "contains":
                    return FilterOperations.Contains;
                case "doesnotcontain":
                    return FilterOperations.NotContains;
                default:
                    return FilterOperations.Contains;
            }
        }
    }
}