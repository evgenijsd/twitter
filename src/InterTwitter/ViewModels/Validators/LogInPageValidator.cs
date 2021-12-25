using FluentValidation;
using InterTwitter.Resources.Strings;
using System.Text.RegularExpressions;

namespace InterTwitter.ViewModels.Validators
{
    public class LogInPageValidator : AbstractValidator<LogInPageViewModel>
    {
        private const string VALID_EMAIL = @"\A[^@]+@([^@\.]+\.)+[^@\.]+\z";
        private const string VALID_PASSWORD = @"^(?=.*[A-Z])(?=.*\d)[\d\D]+$";

        public LogInPageValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(Strings.AlertEmailEmpty)
                .Must(x => x.Contains("@")).WithMessage(Strings.AlertEmailNoA)
                .Must(x => Regex.IsMatch(x, VALID_EMAIL))
                           .WithMessage(Strings.AlertEmailInvalid);
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(Strings.AlertPasswordEmpty)
                .MinimumLength(6).WithMessage(Strings.AlertPasswordLength)
                .Must(x => Regex.IsMatch(x, VALID_PASSWORD))
                           .WithMessage(Strings.AlertPasswordLetterDigit);
        }
    }
}
