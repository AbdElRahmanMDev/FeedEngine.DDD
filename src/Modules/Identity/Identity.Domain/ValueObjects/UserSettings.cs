using Identity.Domain.Models.enums;

namespace Identity.Domain.ValueObjects
{
    public sealed record UserSettings
    {
        public ThemeMode Theme { get; init; }
        public string Language { get; init; } = "en"; // or LanguageCode VO
        public bool NotificationsEnabled { get; init; }
        public PrivacyLevel PrivacyLevel { get; init; }


        private UserSettings()
        {

        }
        private UserSettings(ThemeMode theme, string language, bool notificationsEnabled, PrivacyLevel privacyLevel)
        {
            Theme = theme;
            Language = language;
            NotificationsEnabled = notificationsEnabled;
            PrivacyLevel = privacyLevel;
        }

        public static UserSettings Default() => new()
        {
            Theme = ThemeMode.Light,
            Language = "en",
            NotificationsEnabled = true,
            PrivacyLevel = PrivacyLevel.Public
        };

        public static UserSettings Create(ThemeMode theme, string language, bool notificationsEnabled, PrivacyLevel privacyLevel)
            => new(theme, language, notificationsEnabled, privacyLevel);


    }
}
