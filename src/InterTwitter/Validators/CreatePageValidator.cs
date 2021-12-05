using FluentValidation;
using InterTwitter.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace InterTwitter.Validators
{
    public class CreatePageValidator : AbstractValidator<CreatePageViewModel>
    {
        public CreatePageValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().OnFailure(x => x.MessageErrorName = Resources.Resource.AlertDataIncorrect)
                .MinimumLength(2).OnFailure(x => x.MessageErrorName = Resources.Resource.AlertNameLength)
                .Must(x => Regex.IsMatch(x, @"^[a-zA-Z]+$"))
                                 .OnFailure(x => x.MessageErrorName = Resources.Resource.AlertNameLetter);
            RuleFor(x => x.Email)
                .NotEmpty().OnFailure(x => x.MessageErrorEmail = Resources.Resource.AlertDataIncorrect)
                .Must(x => x.Contains("@")).OnFailure(x => x.MessageErrorEmail = Resources.Resource.AlertEmailNoA)
                .Must(x => Regex.IsMatch(x, @"\A[^@]+@([^@\.]+\.)+[^@\.]+\z"))
                                 .OnFailure(x => x.MessageErrorEmail = Resources.Resource.AlertEmailInvalid);
        }
    }
}
