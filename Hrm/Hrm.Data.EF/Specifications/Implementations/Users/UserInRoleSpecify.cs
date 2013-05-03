using System;
using System.Linq.Expressions;
using Hrm.Data.EF.Models;
using Hrm.Data.EF.Specifications.Contracts;

namespace Hrm.Data.EF.Specifications.Implementations.Users
{
    public class UserInRoleSpecify : ISpecification<User>
    {
        private readonly string username;

        private readonly string rolename;

        public UserInRoleSpecify(string username, string rolename)
        {
            this.username = username;
            this.rolename = rolename;
        }

        #region Implementation of ISpecification<User>

        public Expression<Func<User, bool>> IsSatisfiedBy()
        {
            return x =>
                x.Login.ToLower().Equals(this.username.ToLower()) && 
                x.Role.ToString().ToLower().Contains(rolename.ToLower());
        }

        #endregion
    }
}