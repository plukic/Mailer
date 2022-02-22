using FluentValidation;
using Mailer.Core.Localization;
using Microsoft.Extensions.Localization;

namespace Mailer.Web.Models.Message.Validators
{
    public class ValidEmailValidator : AbstractValidator<string>
    {
        public ValidEmailValidator(IStringLocalizer<MessageComposeViewModelValidator> localizer)
        {

        }

    }
    public class MessageComposeViewModelValidator : AbstractValidator<MessageComposeViewModel>
    {
        public MessageComposeViewModelValidator(IStringLocalizer<MessageComposeViewModelValidator> localizer)
        {
            RuleFor(x => x.To)
                    .NotEmpty()
                    .WithMessage(localizer[LocalizationKeys.FieldIsRequired]);
            RuleForEach(x => x.To)
                .Matches(@"^(?=.{1,254}$)(?=.{1,64}@)[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$")
                .WithMessage((model, x) => localizer[LocalizationKeys.EmailIsNotInValidFormat, x]);
            RuleForEach(x => x.Bcc)
                .Matches(@"^(?=.{1,254}$)(?=.{1,64}@)[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$")
                .WithMessage((model, x) => localizer[LocalizationKeys.EmailIsNotInValidFormat, x])
                .When(x => x.Bcc != null && x.Bcc.Any());
            RuleForEach(x => x.Cc)
                    .Matches(@"^(?=.{1,254}$)(?=.{1,64}@)[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$")
                    .WithMessage((model, x) => localizer[LocalizationKeys.EmailIsNotInValidFormat, x])
                    .When(x => x.Cc != null && x.Cc.Any());
        }
    }
}
