using System;
using System.Collections.Generic;
using Hrm.Data.EF.Models.Base;

namespace Hrm.Data.EF.Models
{
    public class TestResult : BaseModel<long>
    {
        public virtual User User { get; set; }

        public virtual long UserId { get; set; }

        public virtual Test Test { get; set; }

        public virtual long TestId { get; set; }

        public virtual DateTime PassDate { get; set; }

        public virtual ICollection<ResultQuestion> ResultQuestions { get; set; }
    }
}