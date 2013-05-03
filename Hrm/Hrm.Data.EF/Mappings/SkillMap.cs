using Hrm.Data.EF.Models;

namespace Hrm.Data.EF.Mappings
{
    public class SkillMap : BaseModelMap<Skill>
    {
        public SkillMap()
        {
            this.ToTable("Skills");

            this.Property(t => t.Name)
                .HasColumnName("Name")
                .HasMaxLength(250)
                .IsRequired();

            this.Property(t => t.Description)
                .HasMaxLength(250);

            this.Property(p => p.SkillCategoryId).HasColumnName("SkillCategory_id");

            this.HasRequired(t => t.SkillCategory)
                .WithMany(r => r.Skills)
                .HasForeignKey(f => f.SkillCategoryId);
        }
    }
}

