using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Hrm.Core.Entities.Skills;

namespace Hrm.Data.Mappings.Overrides
{
    public class DesignSkillMappingOverride : IAutoMappingOverride<DesignSkill>
    {
        public void Override(AutoMapping<DesignSkill> mapping)
        {
            mapping.Table("DesignSkills");
        }
    }
}