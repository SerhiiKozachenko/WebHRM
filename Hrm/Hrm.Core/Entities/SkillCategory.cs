using Hrm.Core.Entities.Base;
using Iesi.Collections.Generic;

namespace Hrm.Core.Entities
{
    public class SkillCategory : BaseEntity
    {
        public virtual string CategoryName { get; set; }

        public virtual string Description { get; set; }

        public virtual ISet<Skill> Skills { get; set; }

        public virtual ISet<UserSkill> UsersSkills { get; set; } 

        public SkillCategory()
        {
            this.Skills = new HashedSet<Skill>();
            this.UsersSkills = new HashedSet<UserSkill>();
        }
    }
}