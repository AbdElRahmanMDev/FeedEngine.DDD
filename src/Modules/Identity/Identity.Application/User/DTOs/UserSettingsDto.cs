using Identity.Domain.Models.enums;

namespace Identity.Application.User.DTOs
{
    public sealed record UserSettingsDto(
    ThemeMode Theme,
    string Language,
    bool NotificationsEnabled,
    PrivacyLevel PrivacyLevel
    );
}
