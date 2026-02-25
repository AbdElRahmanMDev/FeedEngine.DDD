namespace Identity.Application.User.DTOs;

public record LoginUserDTO
 (Guid UserId,
string Email,
string Token,
int ExpiresIn);
