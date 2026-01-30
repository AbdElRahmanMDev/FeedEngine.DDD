using BuildingBlocks.Application.Messaging;
using Identity.Application.User.DTOs;

namespace Identity.Application.User.Queries.GetUserByUsername;

public sealed record GetUserByUsernameQuery(string Username) : IQuery<UserDto>;