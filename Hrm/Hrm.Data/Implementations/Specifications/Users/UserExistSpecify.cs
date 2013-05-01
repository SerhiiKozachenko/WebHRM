using System;
using System.Linq.Expressions;
using Hrm.Core.Entities;
using Hrm.Core.Interfaces.Specifications.Base;

namespace Hrm.Data.Implementations.Specifications.Users
{
    public class UserExistSpecify : ISpecification<User>
    {
        private readonly string login;

        private readonly string password;

        public UserExistSpecify(string login, string password)
        {
            this.login = login;
            this.password = password;
        }

        #region Implementation of ISpecification<User>

        public Expression<Func<User, bool>> IsSatisfiedBy()
        {
            return x =>
                x.Login.ToLower().Equals(this.login.ToLower()) && 
                x.Password.ToLower().Equals(this.password.ToLower());
        }

        #endregion
    }
}