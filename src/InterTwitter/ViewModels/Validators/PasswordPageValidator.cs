using FluentValidation;
using InterTwitter.Resources.Strings;
using System.Text.RegularExpressions;

namespace InterTwitter.ViewModels.Validators
{
    public class PasswordPageValidator : AbstractValidator<PasswordPageViewModel>
    {
        private const string VALID_PASSWORD = @"^(?=.*[A-Z])(?=.*\d)[\d\D]+$";

        public PasswordPageValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(Strings.AlertPasswordEmpty)
                .MinimumLength(6).WithMessage(Strings.AlertPasswordLength)
                .Must(x => Regex.IsMatch(x, VALID_PASSWORD))
                                 .WithMessage(Strings.AlertPasswordLetterDigit);
            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage(Strings.AlertPasswordNotEqual);
        }
    }
}
