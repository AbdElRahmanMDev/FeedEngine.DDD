using BuildingBlocks.Application.Messaging;

namespace Identity.Application.User.Commands.ChangeUsername;

public sealed record ChangeUsernameCommand(
    string NewUsername
) : ICommand<ChangeUsernameResult>;

public sealed record ChangeUsernameResult(Guid UserId, string NewUsername);