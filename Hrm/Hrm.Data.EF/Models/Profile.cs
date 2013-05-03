using System;
using Hrm.Data.EF.Models.Base;

namespace Hrm.Data.EF.Models
{
    public class Profile : BaseModel<long>
    {
        public virtual string PhoneNumber { get; set; }

        public virtual string Skype { get; set; }

        public virtual DateTime? DateOfBirth { get; set; }

        public virtual string LastJobTitle { get; set; }

        public virtual int? TotalWorkExperience { get; set; }

        public virtual string ResumePath { get; set; }
    }
}