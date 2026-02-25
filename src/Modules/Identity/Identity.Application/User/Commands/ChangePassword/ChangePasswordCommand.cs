using BuildingBlocks.Application.Messaging;

namespace Identity.Application.User.Commands.ChangePassword;

public sealed record ChangePasswordCommand(
    string NewPasswordHash
) : ICommand<ChangePasswordResult>;

public sealed record ChangePasswordResult(Guid UserId);