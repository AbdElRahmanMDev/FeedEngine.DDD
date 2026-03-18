namespace Notification.Application.Abstractions
{
    public static class WelcomeEmailFactory
    {
        public static EmailMessage Create(string email, string firstName)
        {
            var safeFirstName = string.IsNullOrWhiteSpace(firstName)
                ? "there"
                : firstName.Trim();

            var body =
                  $"""
                Hi {safeFirstName},

                Welcome to our platform.
                Your account has been created successfully.

                You can start here:
                https://your-app.com/getting-started

                We are happy to have you with us.
                """;

            return new EmailMessage(
                email,
                "Welcome to our platform",
                body);
        }
    }
}
