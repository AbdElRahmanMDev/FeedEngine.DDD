using Identity.Application.Abstractions.Security;

namespace Identity.Infrastructure.Security;

public class BcryptPasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);

    public bool VerifyHashedPassword(string password, string passwordHash)
        => BCrypt.Net.BCrypt.Verify(password, passwordHash);


}
