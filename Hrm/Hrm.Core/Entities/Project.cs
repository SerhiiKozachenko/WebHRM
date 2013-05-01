using Hrm.Core.Entities.Base;
using Hrm.Core.Enums;
using Iesi.Collections.Generic;

namespace Hrm.Core.Entities
{
    public class Project : BaseEntity
    {
        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual ProjectStatuses Status { get; set; }

        public virtual ISet<Job> Jobs { get; set; } 

        public virtual ISet<User> UsersInProject { get; set; }

        public Project()
        {
            this.Jobs = new HashedSet<Job>();
            this.UsersInProject = new HashedSet<User>();
        }
    }
}