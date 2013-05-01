using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Hrm.Core.Entities;

namespace Hrm.Data.Mappings.Overrides
{
    public class JobSkillMappingOverride : IAutoMappingOverride<JobSkill>
    {
        #region Implementation of IAutoMappingOverride<JobSkills>

        /// <summary>
        /// Alter the automapping for this type
        /// </summary>
        /// <param name="mapping">Automapping</param>
        public void Override(AutoMapping<JobSkill> mapping)
        {
            mapping.Table("JobsSkills");
            mapping.References(p => p.Category).Column("SkillCat_id").LazyLoad();
            mapping.References(p => p.Skill).Column("Skill_id").LazyLoad();
        }

        #endregion
    }
}