using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Domain.Abstraction;
using Identity.Application.Abstractions.Authentication;
using Identity.Application.Abstractions.Security;
using Identity.Application.User.DTOs;
using Identity.Domain;
using Identity.Domain.Models.enums;

namespace Identity.Application.User.Commands.LoginUser;

public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, LoginUserDTO>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public LoginUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }
    public async Task<Result<LoginUserDTO>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null)
            return Result.Failure<LoginUserDTO>(new Error("Auth.InvalidCredentials", "Invalid credentials"));

        if (user.Status == AccountStatus.InActive || user.Status == AccountStatus.Deleted)
            return Result.Failure<LoginUserDTO>(new Error("Auth.UserNotActive", "User is not active"));

        bool valid = _passwordHasher.VerifyHashedPassword(request.Password, user.PasswordHash.Value);

        if (!valid)
            return Result.Failure<LoginUserDTO>(new Error("Auth.InvalidCredentials", "Invalid credentials"));




        var (token, expiresIn) = _jwtProvider.GenerateToken(user);

        return Result.Success(new LoginUserDTO(
                user.Id.Value,
                user.Email.Value,
                token,
                expiresIn
       ));

    }
}
