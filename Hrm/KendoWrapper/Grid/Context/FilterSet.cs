namespace KendoWrapper.Grid.Context
{
    public class FilterSet
    {
        public FilterInfo Filter1 { get; set; }

        public FilterInfo Filter2 { get; set; }

        public FilterLogic Logic { get; set; }

        public static FilterLogic ParseLogic(string logic)
        {
            switch (logic)
            {
                case "and" :
                    return FilterLogic.And;
                case "or":
                    return FilterLogic.Or;

                default: return FilterLogic.And;
            }
        }
    }
}