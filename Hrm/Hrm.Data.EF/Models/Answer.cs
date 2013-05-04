using Hrm.Data.EF.Models.Base;

namespace Hrm.Data.EF.Models
{
    public class Answer : BaseModel<long>
    {
        public virtual string AnswerText { get; set; }

        public virtual bool IsCorrect { get; set; }

        public virtual Question Question { get; set; }

        public virtual long QuestionId { get; set; }
    }
}