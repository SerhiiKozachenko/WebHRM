using Hrm.Data.EF.Models;

namespace Hrm.Data.EF.Mappings
{
    public class UserSkillMap : BaseModelMap<UserSkill>
    {
        public UserSkillMap()
        {
            this.ToTable("UsersSkills");

            this.Property(t => t.Estimate)
                .IsRequired();

            this.Property(p => p.UserId).HasColumnName("User_id");

            this.Property(p => p.SkillCategoryId).HasColumnName("SkillCategory_id");

            this.Property(p => p.SkillId).HasColumnName("Skill_id");

            this.HasRequired(t => t.SkillCategory)
                .WithMany(e => e.UsersSkills)
                .HasForeignKey(f => f.SkillCategoryId);

            this.HasRequired(t => t.User)
                .WithMany(e => e.UsersSkills)
                .HasForeignKey(f => f.UserId);

            this.HasRequired(r => r.Skill)
                .WithMany(m => m.UsersSkills)
                .HasForeignKey(f => f.SkillId);
        }
    }
}

