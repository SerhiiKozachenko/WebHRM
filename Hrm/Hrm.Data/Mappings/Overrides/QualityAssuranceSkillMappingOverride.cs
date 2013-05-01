using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Hrm.Core.Entities.Skills;

namespace Hrm.Data.Mappings.Overrides
{
    public class QualityAssuranceSkillMappingOverride : IAutoMappingOverride<QualityAssuranceSkill>
    {
        public void Override(AutoMapping<QualityAssuranceSkill> mapping)
        {
            mapping.Table("QualityAssuranceSkills");
        }
    }
}