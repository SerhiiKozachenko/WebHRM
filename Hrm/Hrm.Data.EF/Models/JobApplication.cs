using System;
using Hrm.Data.EF.Models.Base;
using Hrm.Data.EF.Models.Enums;

namespace Hrm.Data.EF.Models
{
    public class JobApplication : BaseModel<long>
    {
        public virtual Job Job { get; set; }

        public virtual long JobId { get; set; }

        public virtual User User { get; set; }

        public virtual long UserId { get; set; }

        public virtual DateTime FilingDate { get; set; }

        public virtual int DesiredSalary { get; set; }

        public virtual JobApplicationStatuses Status { get; set; }
    }
}