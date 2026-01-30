namespace Identity.Application.User.DTOs;

public sealed record UserDto(
    Guid UserId,
    string Email,
    string Username,
    bool EmailVerified,
    int Status,
    DateTime CreatedAt,
    DateTime UpdatedAt
);