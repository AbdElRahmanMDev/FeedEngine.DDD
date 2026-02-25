using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Domain.Abstraction;
using Identity.Application.Abstractions.Security;
using Identity.Application.User.DTOs;
using Identity.Domain;

namespace Identity.Application.User.Commands.RegisterUser;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, RegisterUserResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
    }

    public async Task<Result<RegisterUserResult>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var nowUtc = DateTime.UtcNow;

        var passwordHash = _passwordHasher.HashPassword(request.Password);

        if (await _userRepository.EmailExistsAsync(request.Email, cancellationToken))
            return Result.Failure<RegisterUserResult>(
                new Error("User.EmailAlreadyExists", "Email already exists"));

        if (await _userRepository.UsernameExistsAsync(request.Username, cancellationToken))
            return Result.Failure<RegisterUserResult>(
                new Error("User.UsernameExists", "Username already exists"));

        var user = Domain.Models.User.Register(request.Email, request.Username, passwordHash, nowUtc);




        await _userRepository.AddAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(new RegisterUserResult(user.Id.Value, user.Email.Value, user.Username.Value));
    }



}
