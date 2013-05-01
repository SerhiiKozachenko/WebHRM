using Hrm.Core.Entities.Base;

namespace Hrm.Core.Entities
{
    public class UserSkill : BaseEntity
    {
        public virtual User User { get; set; }

        public virtual SkillCategory SkillCategory { get; set; }

        public virtual Skill Skill { get; set; }

        public virtual int Estimate { get; set; }
    }
}