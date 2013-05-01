using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Hrm.Core.Entities;

namespace Hrm.Data.Mappings.Overrides
{
    public class DepartmentMappingOverride : IAutoMappingOverride<Department>
    {
        public void Override(AutoMapping<Department> mapping)
        {
            mapping.Table("Departments");
            mapping.HasManyToMany(m => m.UsersInDepartment).Table("UsersInDepartments");
        }
    }
}