using System;
using System.Linq.Expressions;
using Hrm.Data.EF.Models;
using Hrm.Data.EF.Specifications.Contracts;

namespace Hrm.Data.EF.Specifications.Implementations.Users
{
    public class UserByEmailSpecify: ISpecification<User>
    {
        private readonly string email;

        public UserByEmailSpecify(string email)
        {
            this.email = email;
        }

        #region Implementation of ISpecification<User>

        public Expression<Func<User, bool>> IsSatisfiedBy()
        {
            return x => x.Email.ToLower().Equals(this.email.ToLower());
        }

        #endregion
    }
}