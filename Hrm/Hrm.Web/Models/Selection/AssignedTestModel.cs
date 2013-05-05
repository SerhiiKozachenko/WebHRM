using Hrm.Web.Models.Base;

namespace Hrm.Web.Models.Selection
{
    public class AssignedTestModel : BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public long CategoryId { get; set; }
    }
}