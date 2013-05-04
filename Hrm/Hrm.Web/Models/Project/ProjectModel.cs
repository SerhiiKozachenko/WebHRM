using Hrm.Data.EF.Models.Enums;
using Hrm.Web.Models.Base;

namespace Hrm.Web.Models.Project
{
    public class ProjectModel : BaseModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public ProjectStatuses Status { get; set; }

        public long ProjectFormalizeNameId { get; set; }
    }
}