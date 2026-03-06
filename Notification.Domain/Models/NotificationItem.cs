

using BuildingBlocks.Domain.Abstraction;
using Notification.Domain.Models.enums;
using Notification.Domain.ValueObjects;

namespace Notification.Domain.Models;

public class NotificationItem : Aggregate<NotificationId>
{
    public UserId RecipientUserId { get; }
    public NotificationType Type { get; }
    public NotificationMessage Message { get; }
    public DeepLink Link { get; }
    public DateTimeOffset CreatedAt { get; }
    public DateTimeOffset? ReadAt { get; private set; }

    public bool IsRead => ReadAt is not null;

    private NotificationItem(
        NotificationId id, UserId recipientUserId, NotificationType type,
        NotificationMessage message, DeepLink link, DateTimeOffset createdAt)
    {
        Id = id;
        RecipientUserId = recipientUserId;
        Type = type;
        Message = message;
        Link = link;
        CreatedAt = createdAt;
    }
    private NotificationItem()
    {

    }
    public static NotificationItem Create(UserId recipient, NotificationType type, NotificationMessage msg, DeepLink link)
        => new(new NotificationId(Guid.NewGuid()), recipient, type, msg, link, DateTimeOffset.UtcNow);

    public void MarkRead()
    {
        if (IsRead) return;
        ReadAt = DateTimeOffset.UtcNow;
    }
}
