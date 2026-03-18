namespace Identity.Application.Abstractions.Authentication;

public interface IJwtProvider
{
    public (string token, int expiresIn) GenerateToken(Domain.Models.User user);

    public (string token, int expiresIn) GenerateEmailVerificationToken(Domain.Models.User user);

}
