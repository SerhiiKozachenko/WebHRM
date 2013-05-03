using System.Collections.Generic;
using Hrm.Data.EF.Models.Base;

namespace Hrm.Data.EF.Models
{
    public class ProjectFormalizeName : BaseModel<long>
    {
        public virtual string FormalizeName { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}