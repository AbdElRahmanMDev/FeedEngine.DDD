namespace Notification.Domain.ValueObjects
{
    public sealed record NotificationMessage(string Title, string Body)
    {
        public static NotificationMessage Create(string title, string body)
        {
            return new NotificationMessage(title, body);
        }
    };

}
