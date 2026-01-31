using BuildingBlocks.Application.Messaging;

namespace Identity.Application.User.Commands.VerifyEmail;

public sealed record VerifyEmailCommand(
    Guid UserId
) : ICommand<VerifyEmailResult>;

public sealed record VerifyEmailResult(Guid UserId, bool EmailVerified);

