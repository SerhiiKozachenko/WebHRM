using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Hrm.Core.Entities;

namespace Hrm.Data.Mappings.Overrides
{
    public class UserSkillMappingOverride : IAutoMappingOverride<UserSkill>
    {
        #region Implementation of IAutoMappingOverride<UserSkills>

        /// <summary>
        /// Alter the automapping for this type
        /// </summary>
        /// <param name="mapping">Automapping</param>
        public void Override(AutoMapping<UserSkill> mapping)
        {
            mapping.Table("UsersSkills");
            mapping.References(p => p.SkillCategory).Column("SkillCategory_id").LazyLoad();
            mapping.References(p => p.Skill).Column("Skill_id").LazyLoad();
        }

        #endregion
    }
}