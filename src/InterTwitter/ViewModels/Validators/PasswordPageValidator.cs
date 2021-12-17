using FluentValidation;
using System.Text.RegularExpressions;

namespace InterTwitter.ViewModels.Validators
{
    public class PasswordPageValidator : AbstractValidator<PasswordPageViewModel>
    {
        private const string VALID_PASSWORD = @"^(?=.*[A-Z])(?=.*\d)[\d\D]+$";

        public PasswordPageValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(Resources.Resource.AlertPasswordEmpty)
                .MinimumLength(6).WithMessage(Resources.Resource.AlertPasswordLength)
                .Must(x => Regex.IsMatch(x, VALID_PASSWORD))
                                 .WithMessage(Resources.Resource.AlertPasswordLetterDigit);
            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage(Resources.Resource.AlertPasswordNotEqual);
        }
    }
}
