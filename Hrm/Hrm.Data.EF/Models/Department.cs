using System.Collections.Generic;
using Hrm.Data.EF.Models.Base;

namespace Hrm.Data.EF.Models
{
    public class Department : BaseModel<long>
    {
        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }

        public virtual ICollection<User> Users { get; set; } 
    }
}