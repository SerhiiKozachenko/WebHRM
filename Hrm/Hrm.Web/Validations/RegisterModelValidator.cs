using FluentValidation;
using Hrm.Web.Models.Account;

namespace Hrm.Web.Validations
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
         public RegisterModelValidator()
         {
             RuleFor(x => x.Login).NotEmpty().WithLocalizedMessage(()=>"Login should not be empty");
             RuleFor(x => x.Login).Length(4, 25).WithLocalizedMessage(() => "Login should be at least 4 and no more than 25 characters");
             RuleFor(x => x.Password).NotEmpty().WithLocalizedMessage(() => "Password should not be empty");
             RuleFor(x => x.Password).Length(4, 25).WithLocalizedMessage(() => "Password should be at least 4 and no more than 25 characters");
         }
    }
}