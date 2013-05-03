using Hrm.Data.EF.Models;

namespace Hrm.Data.EF.Mappings
{
    public class JobApplicationMap : BaseModelMap<JobApplication>
    {
        public JobApplicationMap()
        {
            this.ToTable("JobApplications");

            this.Property(p => p.JobId).HasColumnName("Job_id");

            this.Property(p => p.UserId).HasColumnName("User_id");

            this.Property(t => t.FilingDate)
                .IsRequired();

            this.Property(t => t.DesiredSalary)
                .IsOptional();

            this.Property(t => t.Status)
                .IsRequired();

            this.HasRequired(p => p.Job)
                .WithMany(m => m.JobApplications)
                .HasForeignKey(f => f.JobId);

            this.HasRequired(p => p.User)
                .WithMany(m => m.JobApplications)
                .HasForeignKey(f => f.UserId);
        }
    }
}
