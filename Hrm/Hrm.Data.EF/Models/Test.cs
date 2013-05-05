using System.Collections.Generic;
using Hrm.Data.EF.Models.Base;

namespace Hrm.Data.EF.Models
{
    public class Test : BaseModel<long>
    {
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual TestCategory Category { get; set; }

        public virtual long CategoryId { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<TestResult> TestResults { get; set; }

        public virtual ICollection<User> Users { get; set; } 
    }
}