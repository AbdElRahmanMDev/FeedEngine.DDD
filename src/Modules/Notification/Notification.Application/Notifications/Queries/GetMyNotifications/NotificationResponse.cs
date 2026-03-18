using Notification.Domain.Models.enums;

namespace Notification.Application.Notifications.Queries.GetMyNotifications
{
    internal class NotificationResponse
    {
        public Guid Id { get; init; }
        public Guid ActorUserId { get; init; }
        public NotificationType Type { get; init; }
        public string Title { get; init; } = string.Empty;
        public string Body { get; init; } = string.Empty;
        public string Link { get; init; } = string.Empty;
        public DateTimeOffset CreatedAt { get; init; }
    }
}
