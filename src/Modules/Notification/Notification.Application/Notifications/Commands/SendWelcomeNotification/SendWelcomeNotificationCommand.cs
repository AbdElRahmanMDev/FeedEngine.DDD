using BuildingBlocks.Application.Messaging;
using MediatR;

namespace Notification.Application.Notifications.Commands.SendWelcomeNotification
{
    public sealed record SendWelcomeNotificationCommand(
     Guid UserId,
     string Email,
     string UserName
    ) : ICommand<Unit>;
}
