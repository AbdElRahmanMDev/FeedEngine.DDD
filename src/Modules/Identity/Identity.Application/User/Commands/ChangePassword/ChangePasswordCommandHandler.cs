using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Domain.Abstraction;
using Identity.Domain;
using Identity.Domain.ValueObjects;

namespace Identity.Application.User.Commands.ChangePassword;

internal sealed class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand, ChangePasswordResult>
{
    private readonly IUserRepository _userRepository;

    public ChangePasswordCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<ChangePasswordResult>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userId = UserId.Of(request.UserId);
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

            if (user is null)
                return Result.Failure<ChangePasswordResult>(
                    new Error("User.NotFound", "User not found"));

            var nowUtc = DateTime.UtcNow;
            user.ChangePasswordHash(request.NewPasswordHash, nowUtc);

            await _userRepository.SaveChangesAsync(cancellationToken);

            return Result.Success(new ChangePasswordResult(user.Id.Value));
        }
        catch (DomainException ex)
        {
            return Result.Failure<ChangePasswordResult>(
                new Error("User.DomainError", ex.Message));
        }
    }
}