namespace Identity.Application.User.DTOs;

public sealed record RegisterUserResult(
    Guid UserId,
    string Email,
    string Username
);