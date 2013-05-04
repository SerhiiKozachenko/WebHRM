using Hrm.Data.EF.Models.Base;

namespace Hrm.Data.EF.Models
{
    public class ResultAnswer : BaseModel<long>
    {
        public virtual string AnswerText { get; set; }

        public virtual bool IsCorrect { get; set; }

        public virtual bool IsChoisen { get; set; }

        public virtual ResultQuestion ResultQuestion { get; set; }

        public virtual long ResultQuestionId { get; set; }
    }
}