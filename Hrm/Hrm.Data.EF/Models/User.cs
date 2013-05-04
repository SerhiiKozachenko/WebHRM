using Hrm.Data.EF.Models.Base;
using Hrm.Data.EF.Models.Enums;
using System.Collections.Generic;

namespace Hrm.Data.EF.Models
{
    public class User : BaseModel<long>
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

        public virtual long? ProfileId { get; set; }

        public virtual ICollection<JobApplication> JobApplications { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

        public virtual ICollection<Department> Departments { get; set; }

        public virtual ICollection<UserSkill> UsersSkills { get; set; }

        public virtual ICollection<TestResult> TestResults { get; set; } 
    }
}