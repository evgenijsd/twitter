using System.Text.RegularExpressions;
using FluentValidation;

namespace InterTwitter.ViewModels.Validators
{
    public class StartPageValidator : AbstractValidator<StartPageViewModel>
    {
        private const string VALID_NAME = @"^[a-zA-Z\s]+$";
        private const string VALID_EMAIL = @"\A[^@]+@([^@\.]+\.)+[^@\.]+\z";

        public StartPageValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(2).OnFailure(x => x.MessageErrorName = Resources.Resource.AlertNameLength)
                .Must(x => Regex.IsMatch(x, VALID_NAME))
                        .OnFailure(x => x.MessageErrorName = Resources.Resource.AlertNameLetter);
            RuleFor(x => x.Email)
                .Must(x => x.Contains("@")).OnFailure(x => x.MessageErrorEmail = Resources.Resource.AlertEmailNoA)
                .Must(x => Regex.IsMatch(x, VALID_EMAIL))
                        .OnFailure(x => x.MessageErrorEmail = Resources.Resource.AlertEmailInvalid);
        }
    }
}
