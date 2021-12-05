using FluentValidation;
using InterTwitter.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace InterTwitter.Validators
{
    public class LogInPageValidator : AbstractValidator<LogInPageViewModel>
    {
        private const string VALID_EMAIL = @"\A[^@]+@([^@\.]+\.)+[^@\.]+\z";
        private const string VALID_PASSWORD = @"^(?=.*[A-Z])(?=.*\d)[\d\D]+$";

        public LogInPageValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().OnFailure(x => x.MessageErrorEmail = Resources.Resource.AlertDataIncorrect)
                .Must(x => x.Contains("@")).OnFailure(x => x.MessageErrorEmail = Resources.Resource.AlertEmailNoA)
                .Must(x => Regex.IsMatch(x, VALID_EMAIL))
                           .OnFailure(x => x.MessageErrorEmail = Resources.Resource.AlertEmailInvalid);
            RuleFor(x => x.Password)
                .NotEmpty().OnFailure(x => x.MessageErrorPassword = Resources.Resource.AlertDataIncorrect)
                .MinimumLength(6).OnFailure(x => x.MessageErrorPassword = Resources.Resource.AlertPasswordLength)
                .Must(x => Regex.IsMatch(x, VALID_PASSWORD))
                           .OnFailure(x => x.MessageErrorPassword = Resources.Resource.AlertPasswordLetterDigit);
        }
    }
}
