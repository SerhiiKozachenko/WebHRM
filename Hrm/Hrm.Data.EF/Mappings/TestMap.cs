using Hrm.Data.EF.Models;

namespace Hrm.Data.EF.Mappings
{
    public class TestMap : BaseModelMap<Test>
    {
         public TestMap()
         {
             this.ToTable("Tests");

             this.Property(p => p.Name).HasMaxLength(int.MaxValue).IsRequired();
             this.Property(p => p.Description).HasMaxLength(int.MaxValue).IsRequired();
             this.Property(p => p.CategoryId).HasColumnName("Category_id");

             this.HasRequired(r => r.Category)
                 .WithMany(m => m.Tests)
                 .HasForeignKey(f => f.CategoryId);
         }
    }
}