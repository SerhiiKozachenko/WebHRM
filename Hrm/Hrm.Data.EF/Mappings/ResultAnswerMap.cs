using Hrm.Data.EF.Models;

namespace Hrm.Data.EF.Mappings
{
    public class ResultAnswerMap : BaseModelMap<ResultAnswer>
    {
         public ResultAnswerMap()
         {
             this.ToTable("ResultAnswers");

             this.Property(p => p.AnswerText).HasColumnName("Answer");
             this.Property(p => p.IsCorrect).IsRequired();
             this.Property(p => p.IsChoisen).IsRequired();
             this.Property(p => p.ResultQuestionId).HasColumnName("ResultQuestion_id");

             this.HasRequired(r => r.ResultQuestion)
                 .WithMany(m => m.ResultAnswers)
                 .HasForeignKey(f => f.ResultQuestionId);
         }
    }
}