using FluentValidation;

namespace Identity.Application.User.Commands.ChangeEmail;

public sealed class ChangeEmailCommandValidator : AbstractValidator<ChangeEmailCommand>
{
    public ChangeEmailCommandValidator()
    {


        RuleFor(x => x.NewEmail)
            .NotEmpty().WithMessage("New email is required")
            .EmailAddress().WithMessage("Email format is invalid");
    }
}