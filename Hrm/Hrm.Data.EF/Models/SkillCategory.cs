using Hrm.Data.EF.Models.Base;
using System.Collections.Generic;

namespace Hrm.Data.EF.Models
{
    public class SkillCategory : BaseModel<long>
    {
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }

        public virtual ICollection<UserSkill> UsersSkills { get; set; } 
    }
}