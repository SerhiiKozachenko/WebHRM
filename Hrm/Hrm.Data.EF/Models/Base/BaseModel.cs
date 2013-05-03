namespace Hrm.Data.EF.Models.Base
{
    public class BaseModel<T> where T: struct
    {
        public T Id { get; set; }
    }
}
