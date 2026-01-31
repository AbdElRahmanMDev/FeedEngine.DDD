using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Domain.Abstraction;
using Identity.Application.User.DTOs;
using Identity.Domain;
using Identity.Domain.Exceptions;

namespace Identity.Application.User.Commands.RegisterUser;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, RegisterUserResult>
{
    private readonly IUserRepository _userRepository;

    public RegisterUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<RegisterUserResult>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var nowUtc = DateTime.UtcNow;

            var user = Domain.Models.User.Register(request.Email, request.Username, request.Password, nowUtc);

            await _userRepository.AddAsync(user, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);

            return Result.Success(new RegisterUserResult(user.Id.Value));
        }
        catch (DomainException ex)
        {
            return Result.Failure<RegisterUserResult>(
                new Error("User.DomainError", ex.Message));
        }
    }
}
