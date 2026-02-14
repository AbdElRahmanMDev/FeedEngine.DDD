namespace Identity.Domain.Models
{
    public sealed class UserSettings
    {
        public Guid UserId { get; private set; }

        public string Language { get; private set; } = "en";
        public string TimeZone { get; private set; } = "UTC";


        public bool EmailNotificationsEnabled { get; private set; } = true;
        public bool PushNotificationsEnabled { get; private set; } = true;

        public bool IsProfilePrivate { get; private set; } = false;

        public DateTime CreatedAtUtc { get; private set; }
        public DateTime UpdatedAtUtc { get; private set; }

        private UserSettings() { } // EF

        private UserSettings(Guid userId)
        {
            UserId = userId;
            CreatedAtUtc = DateTime.UtcNow;
            UpdatedAtUtc = DateTime.UtcNow;
        }

        public static UserSettings CreateDefaults(Guid userId)
            => new(userId);

        public void Update(
            string language,
            string timeZone,
            bool emailNotificationsEnabled,
            bool pushNotificationsEnabled,
            bool isProfilePrivate)
        {
            Language = language;
            TimeZone = timeZone;
            EmailNotificationsEnabled = emailNotificationsEnabled;
            PushNotificationsEnabled = pushNotificationsEnabled;
            IsProfilePrivate = isProfilePrivate;
            UpdatedAtUtc = DateTime.UtcNow;
        }
    }
}
