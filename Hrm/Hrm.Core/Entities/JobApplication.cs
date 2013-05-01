using System;
using Hrm.Core.Entities.Base;
using Hrm.Core.Enums;

namespace Hrm.Core.Entities
{
    public class JobApplication : BaseEntity
    {
        public virtual Job Job { get; set; }

        public virtual User User { get; set; }

        public virtual DateTime FilingDate { get; set; }

        public virtual int? DesiredSalary { get; set; }

        public virtual JobApplicationStatuses Status { get; set; }
    }
}