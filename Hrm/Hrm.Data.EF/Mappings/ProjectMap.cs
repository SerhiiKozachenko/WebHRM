using Hrm.Data.EF.Models;

namespace Hrm.Data.EF.Mappings
{
    public class ProjectMap : BaseModelMap<Project>
    {
        public ProjectMap()
        {
            this.ToTable("Projects");

            this.Property(t => t.Title)
                .HasMaxLength(250)
                .IsRequired();

            this.Property(t => t.Description)
                .HasMaxLength(int.MaxValue);

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(p => p.ProjectFormalizeNameId).HasColumnName("ProjectFormalizeName_id");

            this.HasRequired(r => r.ProjectFormalizeName)
                .WithMany(m => m.Projects)
                .HasForeignKey(f => f.ProjectFormalizeNameId);
        }
    }
}

