using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Hrm.Core.Entities;

namespace Hrm.Data.Mappings.Overrides
{
    public class SkillMappingOverride : IAutoMappingOverride<Skill>
    {
        #region Implementation of IAutoMappingOverride<SkillName>

        /// <summary>
        /// Alter the automapping for this type
        /// </summary>
        /// <param name="mapping">Automapping</param>
        public void Override(AutoMapping<Skill> mapping)
        {
            mapping.Table("SkillNames");
            mapping.Map(p => p.Name).Column("SkillName");
            //mapping.References(p=>p.SkillCategory).Column("SkillCategory_id").Cascade.Delete();
            mapping.HasMany(m => m.UsersSkills).Cascade.DeleteOrphan();
        }

        #endregion
    }
}