using BuildingBlocks.Application.Messaging;

namespace Identity.Application.User.Commands.DeactivateUser;

public sealed record DeactivateUserCommand(
) : ICommand<DeactivateUserResult>;

public sealed record DeactivateUserResult(Guid UserId);