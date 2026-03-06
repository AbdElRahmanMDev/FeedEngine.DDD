using BuildingBlocks.Application.Messaging;
using Notification.Domain.Models.enums;


namespace Notification.Application.Notifications.Commands.CreateNotification;

public record CreateNotificationCommand(Guid RecipientUserId,
    NotificationType Type,
    string Title,
    string Body,
    string DeepLink) : ICommand<Guid>;

