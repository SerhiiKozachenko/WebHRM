using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Hrm.Core.Entities.Skills;

namespace Hrm.Data.Mappings.Overrides
{
    public class LanguageSkillMappingOverride : IAutoMappingOverride<LanguageSkill>
    {
        public void Override(AutoMapping<LanguageSkill> mapping)
        {
            mapping.Table("LanguageSkills");
        }
    }
}