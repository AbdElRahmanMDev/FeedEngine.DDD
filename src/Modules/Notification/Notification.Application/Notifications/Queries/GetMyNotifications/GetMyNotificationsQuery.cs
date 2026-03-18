using BuildingBlocks.Application.Messaging;
using Notification.Application.Notifications.Queries.GetMyNotifications;

namespace Notification.Application.Notification.Queries.GetNotifications
{
    public sealed record GetMyNotificationsQuery()
     : IQuery<IReadOnlyList<NotificationResponse>>;
}
