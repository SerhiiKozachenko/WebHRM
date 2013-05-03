using Hrm.Data.EF.Models;

namespace Hrm.Data.EF.Mappings
{
    public class SkillCategoryMap : BaseModelMap<SkillCategory>
    {
        public SkillCategoryMap()
        {
            this.ToTable("SkillCategories");

            this.Property(t => t.Name)
                .HasMaxLength(250)
                .IsRequired();

            this.Property(t => t.Description)
                .HasMaxLength(250);
        }
    }
}

