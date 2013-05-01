using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Hrm.Core.Entities;

namespace Hrm.Data.Mappings.Overrides
{
    public class SkillCategoryMappingOverride : IAutoMappingOverride<SkillCategory>
    {
        #region Implementation of IAutoMappingOverride<SkillCategory>

        /// <summary>
        /// Alter the automapping for this type
        /// </summary>
        /// <param name="mapping">Automapping</param>
        public void Override(AutoMapping<SkillCategory> mapping)
        {
            mapping.Table("SkillCategories");
            mapping.HasMany(m => m.Skills).Table("SkillNames").Cascade.All();
            mapping.HasMany(m => m.UsersSkills).Cascade.DeleteOrphan();
        }

        #endregion
    }
}