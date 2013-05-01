using Hrm.Core.Entities.Base;
using Hrm.Core.Enums;
using Iesi.Collections.Generic;

namespace Hrm.Core.Entities
{
    public class User : BaseEntity
    {
        public virtual string Login { get; set; }

        public virtual string Password { get; set; }

        public virtual string Email { get; set; }

        public virtual string LastName { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string MiddleName { get; set; }

        public virtual bool IsConfirmed { get; set; }

        public virtual Roles Role { get; set; }

        public virtual Profile Profile { get; set; }

        public virtual ISet<JobApplication> Applications { get; set; }

        public virtual ISet<Project> Projects { get; set; }

        public virtual ISet<Department> Departments { get; set; }

        public virtual ISet<UserSkill> Skills { get; set; } 

        public User()
        {
            this.Applications = new HashedSet<JobApplication>();
            this.Projects = new HashedSet<Project>();
            this.Departments = new HashedSet<Department>();
            this.Skills = new HashedSet<UserSkill>();
        }
    }
}