using System.Collections.Generic;
using Hrm.Data.EF.Models.Base;

namespace Hrm.Data.EF.Models
{
    public class Job : BaseModel<long>
    {
        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual int Salary { get; set; }

        public virtual Department Department { get; set; }

        public virtual long DepartmentId { get; set; }

        public virtual Project Project { get; set; }

        public virtual long ProjectId { get; set; }

        public virtual ICollection<JobApplication> JobApplications { get; set; }

        public virtual ICollection<JobSkill> JobSkills { get; set; } 
    }
}