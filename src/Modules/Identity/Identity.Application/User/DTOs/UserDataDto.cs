namespace Identity.Application.User.DTOs;

public sealed record UserDataDto(
Guid UserId,
string Email,
string Username);
