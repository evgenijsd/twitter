using FluentValidation;
using System.Text.RegularExpressions;
using Xamarin.Plugins.FluentValidation;

namespace InterTwitter.ViewModels.Validators
{
    public class StartPageValidator : AbstractValidator<StartPageViewModel>
    {
        public StartPageValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(2).OnFailure(x => x.MessageErrorName = Resources.Resource.AlertNameLength)
                .Must(x => Regex.IsMatch(x, @"^[a-zA-Z]+$"))
                        .OnFailure(x => x.MessageErrorName = Resources.Resource.AlertNameLetter);
            RuleFor(x => x.Email)
                .Must(x => x.Contains("@")).OnFailure(x => x.MessageErrorEmail = Resources.Resource.AlertEmailNoA)
                .Must(x => Regex.IsMatch(x, @"\A[^@]+@([^@\.]+\.)+[^@\.]+\z"))
                        .OnFailure(x => x.MessageErrorEmail = Resources.Resource.AlertEmailInvalid);
        }
    }
}
