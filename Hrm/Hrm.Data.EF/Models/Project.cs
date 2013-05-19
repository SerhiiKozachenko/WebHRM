using System;
using Hrm.Data.EF.Models.Base;
using Hrm.Data.EF.Models.Enums;
using System.Collections.Generic;

namespace Hrm.Data.EF.Models
{
    public class Project : BaseModel<long>
    {
        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual DateTime StartDate { get; set; }

        public virtual DateTime EndDate { get; set; }

        public virtual ProjectStatuses Status { get; set; }

        public virtual ProjectFormalizeName ProjectFormalizeName { get; set; }

        public virtual long ProjectFormalizeNameId { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}