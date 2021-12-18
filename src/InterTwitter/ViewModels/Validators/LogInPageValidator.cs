using FluentValidation;
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
                .NotEmpty().WithMessage(Resources.Resource.AlertEmailEmpty)
                .Must(x => x.Contains("@")).WithMessage(Resources.Resource.AlertEmailNoA)
                .Must(x => Regex.IsMatch(x, VALID_EMAIL))
                           .WithMessage(Resources.Resource.AlertEmailInvalid);
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(Resources.Resource.AlertPasswordEmpty)
                .MinimumLength(6).WithMessage(Resources.Resource.AlertPasswordLength)
                .Must(x => Regex.IsMatch(x, VALID_PASSWORD))
                           .WithMessage(Resources.Resource.AlertPasswordLetterDigit);
        }
    }
}
