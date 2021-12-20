using FluentValidation;
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
                .NotEmpty().WithMessage(Resources.Resource.AlertNameEmpty)
                .MinimumLength(2).WithMessage(Resources.Resource.AlertNameLength)
                .Must(x => Regex.IsMatch(x, VALID_NAME))
                                 .WithMessage(Resources.Resource.AlertNameLetter);
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(Resources.Resource.AlertEmailEmpty)
                .Must(x => x.Contains("@")).WithMessage(Resources.Resource.AlertEmailNoA)
                .Must(x => Regex.IsMatch(x, VALID_EMAIL))
                                 .WithMessage(Resources.Resource.AlertEmailInvalid);
        }
    }
}
