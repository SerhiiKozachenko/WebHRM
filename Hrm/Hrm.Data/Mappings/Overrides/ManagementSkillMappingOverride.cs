using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Hrm.Core.Entities.Skills;

namespace Hrm.Data.Mappings.Overrides
{
    public class ManagementSkillMappingOverride : IAutoMappingOverride<ManagementSkill>
    {
        public void Override(AutoMapping<ManagementSkill> mapping)
        {
            mapping.Table("ManagementSkills");
        }
    }
}