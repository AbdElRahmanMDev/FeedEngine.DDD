using BuildingBlocks.Application.Messaging;

namespace Notification.Application.Notifications.Commands.MarkAllNotificationsRead
{
    public sealed record MarkAllNotificationsReadCommand(
    ) : ICommand<int>;
}
