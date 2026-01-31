using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Domain.Abstraction;
using Identity.Domain;
using Identity.Domain.ValueObjects;

namespace Identity.Application.User.Commands.ChangeEmail;

internal sealed class ChangeEmailCommandHandler : ICommandHandler<ChangeEmailCommand, ChangeEmailResult>
{
    private readonly IUserRepository _userRepository;

    public ChangeEmailCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<ChangeEmailResult>> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userId = UserId.Of(request.UserId);
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

            if (user is null)
                return Result.Failure<ChangeEmailResult>(
                    new Error("User.NotFound", "User not found"));

            var nowUtc = DateTime.UtcNow;
            user.ChangeEmail(request.NewEmail, nowUtc);

            await _userRepository.SaveChangesAsync(cancellationToken);

            return Result.Success(new ChangeEmailResult(user.Id.Value, user.Email.Value));
        }
        catch (DomainException ex)
        {
            return Result.Failure<ChangeEmailResult>(
                new Error("User.DomainError", ex.Message));
        }
    }
}