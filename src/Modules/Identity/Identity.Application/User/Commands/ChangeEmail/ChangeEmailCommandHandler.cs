using Identity.Domain;
using Identity.Domain.ValueObjects;

namespace Identity.Application.User.Commands.ChangeEmail;

internal sealed class ChangeEmailCommandHandler : ICommandHandler<ChangeEmailCommand, ChangeEmailResult>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public ChangeEmailCommandHandler(IUserRepository userRepository, ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
        _userRepository = userRepository;
    }

    public async Task<Result<ChangeEmailResult>> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
    {

        var userId = UserId.Of(_currentUserService.UserId!.Value);
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

        if (user is null)
            return Result.Failure<ChangeEmailResult>(
                new Error("User.NotFound", "User not found"));

        if (await _userRepository.EmailExistsAsync(request.NewEmail, cancellationToken))
            return Result.Failure<ChangeEmailResult>(
                new Error("User.EmailAlreadyExists", "Email already exists"));

        var nowUtc = DateTime.UtcNow;
        user.ChangeEmail(request.NewEmail, nowUtc);

        await _userRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(new ChangeEmailResult(user.Id.Value, user.Email.Value));
    }
}