using BuildingBlocks.Application.Messaging;
using Identity.Application.User.DTOs;

namespace Identity.Application.User.Queries.GetUserSettings;

public sealed record GetUserSettingsQuery() : IQuery<UserSettingsDto>;

