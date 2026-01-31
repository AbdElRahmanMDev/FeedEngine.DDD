using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Domain.Abstraction;
using Identity.Domain;
using Identity.Domain.ValueObjects;

namespace Identity.Application.User.Commands.DeactivateUser;

internal sealed class DeactivateUserCommandHandler : ICommandHandler<DeactivateUserCommand, DeactivateUserResult>
{
    private readonly IUserRepository _userRepository;

    public DeactivateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<DeactivateUserResult>> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userId = UserId.Of(request.UserId);
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

            if (user is null)
                return Result.Failure<DeactivateUserResult>(
                    new Error("User.NotFound", "User not found"));

            var nowUtc = DateTime.UtcNow;
            user.Deactivate(nowUtc);

            await _userRepository.SaveChangesAsync(cancellationToken);

            return Result.Success(new DeactivateUserResult(user.Id.Value));
        }
        catch (DomainException ex)
        {
            return Result.Failure<DeactivateUserResult>(
                new Error("User.DomainError", ex.Message));
        }
    }
}