using Hrm.Core.Entities.Base;
using Iesi.Collections.Generic;
using System.Collections.Generic;

namespace Hrm.Core.Entities
{
    public class Department : BaseEntity
    {
        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }

        public virtual ICollection<User> UsersInDepartment { get; set; } 

        public Department()
        {
            //this.Jobs = new HashedSet<Job>();
            //this.UsersInDepartment = new HashedSet<User>();
        }
    }
}