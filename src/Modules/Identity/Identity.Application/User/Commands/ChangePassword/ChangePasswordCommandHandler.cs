using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Domain.Abstraction;
using Identity.Application.Abstractions.Security;
using Identity.Domain;
using Identity.Domain.ValueObjects;

namespace Identity.Application.User.Commands.ChangePassword;

internal sealed class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand, ChangePasswordResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public ChangePasswordCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<ChangePasswordResult>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {

        var userId = UserId.Of(request.UserId);
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

        if (user is null)
            return Result.Failure<ChangePasswordResult>(
                new Error("User.NotFound", "User not found"));

        var nowUtc = DateTime.UtcNow;
        var hash = _passwordHasher.HashPassword(request.NewPasswordHash);

        user.ChangePasswordHash(hash, nowUtc);

        await _userRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(new ChangePasswordResult(user.Id.Value));


    }
}