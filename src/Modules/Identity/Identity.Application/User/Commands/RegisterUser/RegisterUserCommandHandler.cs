using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Domain.Abstraction;
using Identity.Application.Abstractions.Authentication;
using Identity.Application.Abstractions.Security;
using Identity.Application.User.DTOs;
using Identity.Domain;
using Identity.Domain.Models;

namespace Identity.Application.User.Commands.RegisterUser;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, RegisterUserResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;
    private readonly IUserSettingsRepository _userSettingsRepository;
    public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider, IUserSettingsRepository userSettingsRepository)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _userSettingsRepository = userSettingsRepository;
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

        var settings = UserSettings.CreateDefaults(user.Id);
        _userSettingsRepository.Add(settings);


        await _userRepository.AddAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);

        var userData = new UserDataDto(
                                user.Id.Value,
                                user.Email.Value,
                                user.Username.Value
                            );

        var (token, expiresIn) = _jwtProvider.GenerateToken(userData);

        return Result.Success(new RegisterUserResult(
                user.Id.Value,
                user.Email.Value,
                token,
                expiresIn
       ));
    }



}
