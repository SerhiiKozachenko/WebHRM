using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Hrm.Core.Entities;

namespace Hrm.Data.Mappings.Overrides
{
    public class SkillMatrixMappingOverride : IAutoMappingOverride<SkillMatrix>
    {
        #region Implementation of IAutoMappingOverride<SkillMatrix>

        /// <summary>
        /// Alter the automapping for this type
        /// </summary>
        /// <param name="mapping">Automapping</param>
        public void Override(AutoMapping<SkillMatrix> mapping)
        {
            mapping.Table("SkillMatrix");
            mapping.References(m => m.LanguageSkills).LazyLoad().Cascade.All();
            mapping.References(m => m.ManagementSkills).LazyLoad().Cascade.All();
            mapping.References(m => m.ProgrammingSkills).LazyLoad().Cascade.All();
            mapping.References(m => m.DesignSkills).LazyLoad().Cascade.All();
            mapping.References(m => m.QualityAssuranceSkills).LazyLoad().Cascade.All();
        }

        #endregion
    }
}