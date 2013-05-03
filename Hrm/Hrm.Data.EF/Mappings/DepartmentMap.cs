using Hrm.Data.EF.Models;

namespace Hrm.Data.EF.Mappings
{
    public class DepartmentMap : BaseModelMap<Department>
    {
        public DepartmentMap()
        {
            this.ToTable("Departments");

            this.Property(t => t.Title)
                .HasMaxLength(250)
                .IsRequired();

            this.Property(t => t.Description)
                .HasMaxLength(int.MaxValue);
        }
    }
}
