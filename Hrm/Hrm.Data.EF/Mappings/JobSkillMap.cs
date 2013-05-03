using Hrm.Data.EF.Models;

namespace Hrm.Data.EF.Mappings
{
    public class JobSkillMap : BaseModelMap<JobSkill>
    {
        public JobSkillMap()
        {
            this.ToTable("JobsSkills");

            this.Property(p => p.JobId).HasColumnName("Job_id");

            this.Property(p => p.SkillCategoryId).HasColumnName("SkillCategory_id");

            this.Property(p => p.SkillId).HasColumnName("Skill_id");

            this.HasRequired(r => r.Job)
                .WithMany(m => m.JobSkills)
                .HasForeignKey(f => f.JobId);

            this.Property(t => t.Estimate)
                .IsRequired();
        }
    }
}

