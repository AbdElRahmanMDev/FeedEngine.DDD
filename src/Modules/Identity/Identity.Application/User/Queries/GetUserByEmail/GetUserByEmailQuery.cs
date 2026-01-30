using BuildingBlocks.Application.Messaging;
using Identity.Application.User.DTOs;

namespace Identity.Application.User.Queries.GetUserByEmail;

public sealed record GetUserByEmailQuery(string Email) : IQuery<UserDto>;