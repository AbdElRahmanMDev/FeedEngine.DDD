using BuildingBlocks.Application.Messaging;
using Identity.Application.User.DTOs;

namespace Identity.Application.User.Queries.GetUserById;

public sealed record GetUserByIdQuery() : IQuery<UserDto>;