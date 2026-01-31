using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Domain.Abstraction;
using Identity.Domain;
using Identity.Domain.ValueObjects;

namespace Identity.Application.User.Commands.ChangeUsername;

internal sealed class ChangeUsernameCommandHandler : ICommandHandler<ChangeUsernameCommand, ChangeUsernameResult>
{
    private readonly IUserRepository _userRepository;

    public ChangeUsernameCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<ChangeUsernameResult>> Handle(ChangeUsernameCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userId = UserId.Of(request.UserId);
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

            if (user is null)
                return Result.Failure<ChangeUsernameResult>(
                    new Error("User.NotFound", "User not found"));

            var nowUtc = DateTime.UtcNow;
            user.ChangeUsername(request.NewUsername, nowUtc);

            await _userRepository.SaveChangesAsync(cancellationToken);

            return Result.Success(new ChangeUsernameResult(user.Id.Value, user.Username.Value));
        }
        catch (DomainException ex)
        {
            return Result.Failure<ChangeUsernameResult>(
                new Error("User.DomainError", ex.Message));
        }
    }
}