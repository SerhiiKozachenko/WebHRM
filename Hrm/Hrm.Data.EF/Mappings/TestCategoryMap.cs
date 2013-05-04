using Hrm.Data.EF.Models;

namespace Hrm.Data.EF.Mappings
{
    public class TestCategoryMap : BaseModelMap<TestCategory>
    {
         public TestCategoryMap()
         {
             this.ToTable("TestCategories");

             this.Property(p => p.Name).HasMaxLength(int.MaxValue).IsRequired();
         }
    }
}