using System.Collections.Generic;
using Hrm.Data.EF.Models.Base;

namespace Hrm.Data.EF.Models
{
    public class Question : BaseModel<long>
    {
        public virtual string QuestionText { get; set; }

        public virtual byte TimeToAnswer { get; set; }

        public virtual Test Test { get; set; }

        public virtual long TestId { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

    }
}