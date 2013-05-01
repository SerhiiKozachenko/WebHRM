using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Hrm.Core.Entities.Skills;

namespace Hrm.Data.Mappings.Overrides
{
    public class ProgrammingSkillMappingOverride : IAutoMappingOverride<ProgrammingSkill>
    {
        public void Override(AutoMapping<ProgrammingSkill> mapping)
        {
            mapping.Table("ProgrammingSkills");
        }
    }
}