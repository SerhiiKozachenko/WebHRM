using Hrm.Data.EF.Models;

namespace Hrm.Data.EF.Mappings
{
    public class QuestionMap : BaseModelMap<Question>
    {
         public QuestionMap()
         {
             this.ToTable("Questions");

             this.Property(p => p.QuestionText).HasColumnName("Question").HasMaxLength(int.MaxValue).IsRequired();
             this.Property(p => p.TimeToAnswer).IsRequired();
             this.Property(p => p.TestId).HasColumnName("Test_id");

             this.HasRequired(r => r.Test)
                 .WithMany(m => m.Questions)
                 .HasForeignKey(f => f.TestId)
                 .WillCascadeOnDelete(true);
         }
    }
}