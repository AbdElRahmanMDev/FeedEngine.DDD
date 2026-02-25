using FluentValidation;

namespace Identity.Application.User.Commands.ChangePassword;

public sealed class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {

        RuleFor(x => x.NewPasswordHash)
            .NotEmpty().WithMessage("New password hash is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters");
    }
}