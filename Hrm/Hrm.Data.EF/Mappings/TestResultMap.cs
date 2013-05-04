using Hrm.Data.EF.Models;

namespace Hrm.Data.EF.Mappings
{
    public class TestResultMap : BaseModelMap<TestResult>
    {
         public TestResultMap()
         {
             this.ToTable("TestResults");

             this.Property(p => p.UserId).HasColumnName("User_id");
             this.Property(p => p.TestId).HasColumnName("Test_id");
             this.Property(p => p.PassDate).IsRequired();

             this.HasRequired(r => r.User)
                 .WithMany(m => m.TestResults)
                 .HasForeignKey(f => f.UserId);

             this.HasRequired(r => r.Test)
                 .WithMany(m => m.TestResults)
                 .HasForeignKey(f => f.TestId);
         }
    }
}