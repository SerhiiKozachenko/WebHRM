using Hrm.Data.EF.Models;

namespace Hrm.Data.EF.Mappings
{
    public class JobMap : BaseModelMap<Job>
    {
        public JobMap()
        {
            this.ToTable("Jobs");

            this.Property(p => p.DepartmentId).HasColumnName("Department_id");

            this.Property(p => p.ProjectId).HasColumnName("Project_id");

            this.Property(t => t.Title)
                .HasMaxLength(250)
                .IsRequired();

            this.Property(t => t.Description)
                .HasMaxLength(int.MaxValue)
                .IsRequired();

            this.Property(t => t.Salary)
                .IsOptional();

            this.HasRequired(t => t.Department)
                .WithMany(c => c.Jobs)
                .HasForeignKey(f => f.DepartmentId);

            this.HasRequired(t => t.Project)
                .WithMany(c => c.Jobs)
                .HasForeignKey(f => f.ProjectId);

            this.HasMany(m => m.SelectedCandidates)
                .WithMany(m => m.Jobs)
                .Map(v => v.ToTable("SelectedCandidates")
                           .MapLeftKey("Job_id")
                           .MapRightKey("User_id"));
        }
    }
}