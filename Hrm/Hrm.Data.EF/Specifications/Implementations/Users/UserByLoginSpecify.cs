using System;
using System.Linq.Expressions;
using Hrm.Data.EF.Models;
using Hrm.Data.EF.Specifications.Contracts;

namespace Hrm.Data.EF.Specifications.Implementations.Users
{
    public class UserByLoginSpecify : ISpecification<User>
    {
        private readonly string login;

        public UserByLoginSpecify(string login)
        {
            this.login = login;
        }

        #region Implementation of ISpecification<User>

        public Expression<Func<User, bool>> IsSatisfiedBy()
        {
            return x => x.Login.ToLower().Equals(this.login.ToLower());
        }

        #endregion
    }
}