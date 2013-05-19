using System.Collections.Generic;
using Hrm.Data.EF.Models.Base;

namespace Hrm.Data.EF.Models
{
    public class Skill : BaseModel<long>
    {
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual SkillCategory SkillCategory { get; set; }

        public virtual long SkillCategoryId { get; set; }

        public virtual ICollection<UserSkill> UsersSkills { get; set; }

        public virtual ICollection<Test> Tests { get; set; } 
    }
}