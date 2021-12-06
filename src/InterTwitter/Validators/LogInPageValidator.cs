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
                .Must(x => Regex.IsMatch(x, VALID_EMAIL))
                           .OnFailure(x => x.MessageErrorEmail = Resources.Resource.AlertEmailInvalid)
                .Must(x => x.Contains("@")).OnFailure(x => x.MessageErrorEmail = Resources.Resource.AlertEmailNoA)
                .NotEmpty().OnFailure(x => x.MessageErrorEmail = Resources.Resource.AlertEmailEmpty);
            RuleFor(x => x.Password)
                .Must(x => Regex.IsMatch(x, VALID_PASSWORD))
                           .OnFailure(x => x.MessageErrorPassword = Resources.Resource.AlertPasswordLetterDigit)
                .MinimumLength(6).OnFailure(x => x.MessageErrorPassword = Resources.Resource.AlertPasswordLength)
                .NotEmpty().OnFailure(x => x.MessageErrorPassword = Resources.Resource.AlertPasswordEmpty);
        }
    }
}
