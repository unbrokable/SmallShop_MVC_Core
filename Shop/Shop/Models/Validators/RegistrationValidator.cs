using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models.Validators
{
    public class RegistrationValidator : AbstractValidator<RegistrationModel>
    {
        public RegistrationValidator()
        {
            RuleFor(i => i.Login).NotNull().MinimumLength(3).WithMessage("Login is too short");
            RuleFor(i => i.Email).NotNull().EmailAddress().WithMessage("It is not  email");
            RuleFor(i => i.Password).MinimumLength(5).Equal(i => i.ConfirmPassword).WithMessage("Dont match");
        }

    }
}
