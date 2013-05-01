using Hrm.Core.Entities.Base;
using Iesi.Collections.Generic;

namespace Hrm.Core.Entities
{
    public class Department : BaseEntity
    {
        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual ISet<Job> Jobs { get; set; }

        public virtual ISet<User> UsersInDepartment { get; set; } 

        public Department()
        {
            this.Jobs = new HashedSet<Job>();
            this.UsersInDepartment = new HashedSet<User>();
        }
    }
}