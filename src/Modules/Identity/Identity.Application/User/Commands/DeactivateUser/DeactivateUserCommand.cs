using BuildingBlocks.Application.Messaging;

namespace Identity.Application.User.Commands.DeactivateUser;

public sealed record DeactivateUserCommand(
    Guid UserId
) : ICommand<DeactivateUserResult>;

public sealed record DeactivateUserResult(Guid UserId);