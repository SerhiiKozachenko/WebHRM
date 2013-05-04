using Hrm.Data.EF.Models;

namespace Hrm.Data.EF.Mappings
{
    public class AnswerMap : BaseModelMap<Answer>
    {
         public AnswerMap()
         {
             this.ToTable("Answers");

             this.Property(p => p.AnswerText).HasColumnName("Answer").HasMaxLength(int.MaxValue).IsRequired();
             this.Property(p => p.IsCorrect).IsRequired();
             this.Property(p => p.QuestionId).HasColumnName("Question_id");

             this.HasRequired(r => r.Question)
                 .WithMany(m => m.Answers)
                 .HasForeignKey(f => f.QuestionId)
                 .WillCascadeOnDelete(true);
         }
    }
}