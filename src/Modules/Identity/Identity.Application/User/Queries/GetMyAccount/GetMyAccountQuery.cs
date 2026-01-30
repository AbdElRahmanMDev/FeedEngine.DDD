using BuildingBlocks.Application.Messaging;
using Identity.Application.User.DTOs;

namespace Identity.Application.User.Queries.GetMyAccount;

public sealed record GetMyAccountQuery(Guid UserId) : IQuery<UserAccountDto>;