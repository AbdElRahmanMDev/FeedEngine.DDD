using Identity.Application.User.DTOs;

namespace Identity.Application.Abstractions.Authentication;

public interface IJwtProvider
{
    public (string token, int expiresIn) GenerateToken(UserDataDto user);

}
