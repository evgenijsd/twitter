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
                .Must(x => Regex.IsMatch(x, VALID_NAME))
                                 .OnFailure(x => x.MessageErrorName = Resources.Resource.AlertNameLetter)
                .MinimumLength(2).OnFailure(x => x.MessageErrorName = Resources.Resource.AlertNameLength)
                .NotEmpty().OnFailure(x => x.MessageErrorName = Resources.Resource.AlertNameEmpty);
            RuleFor(x => x.Email)
                .Must(x => Regex.IsMatch(x, VALID_EMAIL))
                                 .OnFailure(x => x.MessageErrorEmail = Resources.Resource.AlertEmailInvalid)
                .Must(x => x.Contains("@")).OnFailure(x => x.MessageErrorEmail = Resources.Resource.AlertEmailNoA)
                .NotEmpty().OnFailure(x => x.MessageErrorEmail = Resources.Resource.AlertEmailEmpty);
        }
    }
}
