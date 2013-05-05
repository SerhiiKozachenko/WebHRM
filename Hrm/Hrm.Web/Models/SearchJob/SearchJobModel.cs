using Hrm.Web.Models.Base;

namespace Hrm.Web.Models.SearchJob
{
    public class SearchJobModel : BaseModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int Salary { get; set; }

        public long DepartmentId { get; set; }

        public long ProjectFormalizeNameId { get; set; }
    }
}