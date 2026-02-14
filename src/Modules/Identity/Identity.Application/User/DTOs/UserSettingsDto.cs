namespace Identity.Application.User.DTOs
{
    public sealed record UserSettingsDto(
     string Language,
     string TimeZone,
     bool EmailNotificationsEnabled,
     bool PushNotificationsEnabled,
     bool IsProfilePrivate
    );
}
