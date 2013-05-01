using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Hrm.Core.Entities;
using Hrm.Core.Enums;

namespace Hrm.Data.Mappings.Overrides
{
    public class ProjectMappingOverride : IAutoMappingOverride<Project>
    {
        #region Implementation of IAutoMappingOverride<Project>

        /// <summary>
        /// Alter the automapping for this type
        /// </summary>
        /// <param name="mapping">Automapping</param>
        public void Override(AutoMapping<Project> mapping)
        {
            mapping.Table("Projects");
            mapping.Map(p => p.Status).CustomType<ProjectStatuses>();
            mapping.HasManyToMany(m => m.UsersInProject).Table("UsersInProjects");
        }

        #endregion
    }
}