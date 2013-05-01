using Hrm.Core.Entities.Base;
using Hrm.Core.Entities.Skills;

namespace Hrm.Core.Entities
{
    public class SkillMatrix : BaseEntity
    {
        public virtual LanguageSkill LanguageSkills { get; set; }

        public virtual ManagementSkill ManagementSkills { get; set; }

        public virtual ProgrammingSkill ProgrammingSkills { get; set; }

        public virtual DesignSkill DesignSkills { get; set; }

        public virtual QualityAssuranceSkill QualityAssuranceSkills { get; set; }
    }
}