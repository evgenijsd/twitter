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
                .Must(x => Regex.IsMatch(x, VALID_PASSWORD))
                                 .OnFailure(x => x.MessageErrorPassword = Resources.Resource.AlertPasswordLetterDigit)
                .MinimumLength(6).OnFailure(x => x.MessageErrorPassword = Resources.Resource.AlertPasswordLength)
                .NotEmpty().OnFailure(x => x.MessageErrorPassword = Resources.Resource.AlertPasswordEmpty);
            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).OnFailure(x => x.MessageErrorConfirmPassword = Resources.Resource.AlertPasswordNotEqual);
        }
    }
}
