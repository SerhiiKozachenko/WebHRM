using System.Collections.Generic;
using Hrm.Data.EF.Models.Base;

namespace Hrm.Data.EF.Models
{
    public class ResultQuestion : BaseModel<long>
    {
        public virtual string QuestionText { get; set; }

        public virtual TestResult TestResult { get; set; }

        public virtual long TestResultId { get; set; }

        public virtual ICollection<ResultAnswer> ResultAnswers { get; set; }
    }
}