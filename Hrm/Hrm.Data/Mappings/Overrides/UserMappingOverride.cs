using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Hrm.Core.Entities;
using Hrm.Core.Enums;

namespace Hrm.Data.Mappings.Overrides
{
    public class UserMappingOverride : IAutoMappingOverride<User>
    {
        public void Override(AutoMapping<User> mapping)
        {
            mapping.Table("Users");
            mapping.Map(p => p.Role).CustomType<Roles>();
            mapping.References(m => m.Profile).LazyLoad().Cascade.All();
            mapping.HasManyToMany(m => m.Departments).Table("UsersInDepartments");
            mapping.HasManyToMany(m => m.Projects).Table("UsersInProjects");
            mapping.HasMany(m => m.Skills).Table("UsersSkills");
        }
    }
}