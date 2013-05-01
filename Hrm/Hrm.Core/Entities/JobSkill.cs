using Hrm.Core.Entities.Base;

namespace Hrm.Core.Entities
{
    public class JobSkill : BaseEntity
    {
        public virtual SkillCategory Category { get; set; }

        public virtual Skill Skill { get; set; }

        public virtual int Estimate { get; set; } 
    }
}