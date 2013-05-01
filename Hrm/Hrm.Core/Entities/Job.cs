using Hrm.Core.Entities.Base;
using Iesi.Collections.Generic;

namespace Hrm.Core.Entities
{
    public class Job : BaseEntity
    {
        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual int? Salary { get; set; }

        public virtual Department Department { get; set; }

        public virtual Project Project { get; set; }

        public virtual SkillMatrix RequiredSkillMatrix { get; set; }

        public virtual ISet<JobApplication> Applications { get; set; }

        public virtual ISet<JobSkill> Skills { get; set; } 

        public Job()
        {
            this.Applications = new HashedSet<JobApplication>();
            this.Skills = new HashedSet<JobSkill>();
        }
    }
}