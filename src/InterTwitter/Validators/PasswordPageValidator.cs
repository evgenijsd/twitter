using FluentValidation;
using InterTwitter.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace InterTwitter.Validators
{
    public class PasswordPageValidator : AbstractValidator<PasswordPageViewModel>
    {
        public PasswordPageValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty().OnFailure(x => x.MessageErrorPassword = Resources.Resource.AlertDataIncorrect)
                .MinimumLength(6).OnFailure(x => x.MessageErrorPassword = Resources.Resource.AlertPasswordLength)
                .Must(x => Regex.IsMatch(x, @"^(?=.*[A-Z])(?=.*\d)[\d\D]+$"))
                                 .OnFailure(x => x.MessageErrorPassword = Resources.Resource.AlertPasswordLetterDigit);
            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).OnFailure(x => x.MessageErrorConfirmPassword = Resources.Resource.AlertPasswordNotEqual);
        }
    }
}
