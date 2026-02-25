using Identity.Domain;
using Identity.Domain.ValueObjects;

namespace Identity.Application.User.Commands.DeactivateUser;

internal sealed class DeactivateUserCommandHandler : ICommandHandler<DeactivateUserCommand, DeactivateUserResult>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;
    public DeactivateUserCommandHandler(IUserRepository userRepository, ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Result<DeactivateUserResult>> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
    {

        var userId = UserId.Of(_currentUserService.UserId!.Value);
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

        if (user is null)
            return Result.Failure<DeactivateUserResult>(
                new Error("User.NotFound", "User not found"));

        var nowUtc = DateTime.UtcNow;
        user.Deactivate(nowUtc);

        await _userRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(new DeactivateUserResult(user.Id.Value));


    }
}