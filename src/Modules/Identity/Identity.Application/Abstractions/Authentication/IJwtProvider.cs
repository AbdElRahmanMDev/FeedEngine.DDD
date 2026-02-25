namespace Identity.Application.Abstractions.Authentication;

public interface IJwtProvider
{
    public (string token, int expiresIn) GenerateToken(Domain.Models.User user);

}
