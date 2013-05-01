using Hrm.Web.Validations;
using FluentValidation.Attributes;

namespace Hrm.Web.Models.Account
{
    [Validator(typeof(RegisterModelValidator))]
    public class RegisterModel
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string RepeatPassword { get; set; }

        public string Email { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }
    }
}