using BuildingBlocks.Application.Messaging;

namespace Identity.Application.User.Commands.UpdateMySettings;

public class UpdateMySettingsCommand(string? Language,
string? TimeZone,
bool EmailNotificationsEnabled,
bool PushNotificationsEnabled,
bool IsProfilePrivate) : ICommand;

