using Identity.Domain;
using Identity.Domain.ValueObjects;

namespace Identity.Application.User.Commands.ChangeUsername;

internal sealed class ChangeUsernameCommandHandler : ICommandHandler<ChangeUsernameCommand, ChangeUsernameResult>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public ChangeUsernameCommandHandler(IUserRepository userRepository, ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Result<ChangeUsernameResult>> Handle(ChangeUsernameCommand request, CancellationToken cancellationToken)
    {
        var userId = UserId.Of(_currentUserService.UserId!.Value);
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

        if (user is null)
            return Result.Failure<ChangeUsernameResult>(
                new Error("User.NotFound", "User not found"));

        if (await _userRepository.UsernameExistsAsync(request.NewUsername, cancellationToken))
            return Result.Failure<ChangeUsernameResult>(
                new Error("User.UsernameExists", "Username already exists"));

        var nowUtc = DateTime.UtcNow;
        user.ChangeUsername(request.NewUsername, nowUtc);

        await _userRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(new ChangeUsernameResult(user.Id.Value, user.Username.Value));
    }
}