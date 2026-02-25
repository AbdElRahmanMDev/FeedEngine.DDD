using BuildingBlocks.Application.Messaging;
using Identity.Domain.Models.enums;
using MediatR;

namespace Identity.Application.User.Commands.UpdateMySettings;

public record UpdateMySettingsCommand(
string? Language,
ThemeMode? Theme,
bool? NotificationsEnabled,
PrivacyLevel? PrivacyLevel
) : ICommand<Unit>;

