namespace Notification.Domain.ValueObjects
{
    public sealed record DeepLink(string Value)
    {
        public static DeepLink Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Deep link cannot be empty.", nameof(value));

            return new DeepLink(value.Trim());
        }

        public override string ToString() => Value;
    }
}
