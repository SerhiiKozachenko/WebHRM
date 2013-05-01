using System;
using Hrm.Core.Entities.Base;

namespace Hrm.Core.Entities
{
    public class Profile : BaseEntity
    {
        public virtual string PhoneNumber { get; set; }

        public virtual string Skype { get; set; }

        public virtual DateTime? DateOfBirth { get; set; }

        public virtual string LastJobTitle { get; set; }

        public virtual int? TotalWorkExperience { get; set; }

        public virtual string ResumePath { get; set; }

        public virtual SkillMatrix SkillMatrix { get; set; }

        public Profile()
        {
        }

        public Profile(SkillMatrix skillMatrix)
        {
            this.SkillMatrix = skillMatrix;
        }
    }
}