using FluentValidation;
using InterTwitter.Resources.Strings;
using System.Text.RegularExpressions;

namespace InterTwitter.ViewModels.Validators
{
    public class CreatePageValidator : AbstractValidator<CreatePageViewModel>
    {
        private const string VALID_NAME = @"^[a-zA-Z\s]+$";
        private const string VALID_EMAIL = @"\A[^@]+@([^@\.]+\.)+[^@\.]+\z";

        public CreatePageValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(Strings.AlertNameEmpty)
                .MinimumLength(2).WithMessage(Strings.AlertNameLength)
                .Must(x => Regex.IsMatch(x, VALID_NAME))
                                 .WithMessage(Strings.AlertNameLetter);
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(Strings.AlertEmailEmpty)
                .Must(x => x.Contains("@")).WithMessage(Strings.AlertEmailNoA)
                .Must(x => Regex.IsMatch(x, VALID_EMAIL))
                                 .WithMessage(Strings.AlertEmailInvalid);
        }
    }
}
