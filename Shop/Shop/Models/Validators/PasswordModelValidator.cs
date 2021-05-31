using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models.Validators
{
    public class PasswordModelValidator : AbstractValidator<PasswordModel>
    {
        public PasswordModelValidator()
        {
            RuleFor(i => i.NewPassword).MinimumLength(5)
                .Equal(i => i.NewConfirmPassword).WithMessage("Dont match")
                .NotEqual(i => i.Password).WithMessage("Cant be the same like old password");
        }
    }
}
