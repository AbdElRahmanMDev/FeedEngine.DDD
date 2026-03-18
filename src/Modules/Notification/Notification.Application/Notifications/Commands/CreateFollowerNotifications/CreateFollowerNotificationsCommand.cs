using BuildingBlocks.Application.Messaging;
using MediatR;
using Notification.Domain.ValueObjects;

namespace Notification.Application.Notifications.Commands.CreateFollowerNotifications
{
    public record CreateFollowerNotificationsCommand(UserId UserId) : IQuery<Unit>;

}
