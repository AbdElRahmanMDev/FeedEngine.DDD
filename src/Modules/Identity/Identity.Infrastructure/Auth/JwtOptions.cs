

namespace Identity.Infrastructure.Auth;

public sealed class JwtOptions
{
    public string Issuer { get; init; } = default!;
    public string Audience { get; init; } = default!;
    public string SecretKey { get; init; } = default!;
    public int ExpiresInMinutes { get; init; } = 60;
}
