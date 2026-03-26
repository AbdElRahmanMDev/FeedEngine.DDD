using Identity.Application.User.DTOs;

namespace Identity.Application.User.Queries.GetUserById;

public sealed record GetCurrentUserQuery() : IQuery<UserDto>;