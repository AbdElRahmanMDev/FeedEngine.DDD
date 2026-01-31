using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Domain.Abstraction;
using Identity.Domain;
using Identity.Domain.ValueObjects;

namespace Identity.Application.User.Commands.VerifyEmail;

internal sealed class VerifyEmailCommandHandler : ICommandHandler<VerifyEmailCommand, VerifyEmailResult>
{
    private readonly IUserRepository _userRepository;

    public VerifyEmailCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<VerifyEmailResult>> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userId = UserId.Of(request.UserId);
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

            if (user is null)
                return Result.Failure<VerifyEmailResult>(
                    new Error("User.NotFound", "User not found"));

            user.VerifyEmail();

            await _userRepository.SaveChangesAsync(cancellationToken);

            return Result.Success(new VerifyEmailResult(user.Id.Value, user.EmailVerified));
        }
        catch (DomainException ex)
        {
            return Result.Failure<VerifyEmailResult>(
                new Error("User.DomainError", ex.Message));
        }
    }
}