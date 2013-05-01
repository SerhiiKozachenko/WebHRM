using System.Linq;

namespace KendoWrapper.Grid.Filtering.Interfaces
{
    public interface IFilterable<T>
    {
        IQueryable<T> Filter(string field, string value, IQueryable<T> query);
    }
}