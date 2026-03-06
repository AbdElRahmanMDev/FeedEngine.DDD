using BuildingBlocks.Application.Messaging;
using MediatR;

namespace Notification.Application.Notifications.Commands.MarkNotificationRead
{
    public sealed record MarkNotificationReadCommand(
     Guid NotificationId
        ) : ICommand<Unit>;
}
