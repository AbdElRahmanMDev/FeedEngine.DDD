using BuildingBlocks.Application.Messaging;

namespace Identity.Application.User.Commands.ChangeEmail;

public sealed record ChangeEmailCommand(
    Guid UserId,
    string NewEmail
) : ICommand<ChangeEmailResult>;

public sealed record ChangeEmailResult(Guid UserId, string NewEmail);