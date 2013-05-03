using Hrm.Core.Entities.Base;
using Iesi.Collections.Generic;
using System.Collections.Generic;

namespace Hrm.Core.Entities
{
    public class Skill : BaseEntity
    {
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual SkillCategory SkillCategory { get; set; }

        public virtual ICollection<UserSkill> UsersSkills { get; set; }

        public Skill()
        {
            //this.UsersSkills = new HashedSet<UserSkill>();
        }
    }
}