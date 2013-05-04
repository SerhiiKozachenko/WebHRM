using Hrm.Data.EF.Models;

namespace Hrm.Data.EF.Mappings
{
    public class ResultQuestionMap : BaseModelMap<ResultQuestion>
    {
         public ResultQuestionMap()
         {
             this.ToTable("ResultQuestions");

             this.Property(p => p.QuestionText).HasColumnName("Question");
             this.Property(p => p.TestResultId).HasColumnName("TestResult_id");

             this.HasRequired(r => r.TestResult)
                 .WithMany(m => m.ResultQuestions)
                 .HasForeignKey(f => f.TestResultId);
         }
    }
}