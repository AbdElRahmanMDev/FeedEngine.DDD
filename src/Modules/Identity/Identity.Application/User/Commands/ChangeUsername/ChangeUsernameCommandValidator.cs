using FluentValidation;

namespace Identity.Application.User.Commands.ChangeUsername;

public sealed class ChangeUsernameCommandValidator : AbstractValidator<ChangeUsernameCommand>
{
    public ChangeUsernameCommandValidator()
    {


        RuleFor(x => x.NewUsername)
            .NotEmpty().WithMessage("New username is required")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters")
            .MaximumLength(50).WithMessage("Username must not exceed 50 characters")
            .Matches(@"^[a-zA-Z0-9_-]+$").WithMessage("Username can only contain letters, numbers, underscores, and hyphens");
    }
}