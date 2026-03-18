using BuildingBlocks.Domain.Abstraction;
using Notification.Domain.Models.enums;
using Notification.Domain.ValueObjects;

namespace Notification.Domain.Models;

public class NotificationItem : Aggregate<NotificationId>
{
    public UserId ActorUserId { get; }
    public UserId RecipientUserId { get; }
    public NotificationType Type { get; }
    public NotificationMessage Message { get; }
    public DeepLink Link { get; }
    public DateTimeOffset CreatedAt { get; }
    public DateTimeOffset? ReadAt { get; private set; }
    public DateTimeOffset? EmailSentAt { get; private set; }

    public bool IsEmailSent => EmailSentAt is not null;

    public bool IsRead => ReadAt is not null;

    private NotificationItem(
        NotificationId id, UserId actorUserId, UserId recipientUserId, NotificationType type,
        NotificationMessage message, DeepLink link, DateTimeOffset createdAt)
    {
        Id = id;
        RecipientUserId = recipientUserId;
        ActorUserId = actorUserId;
        Type = type;
        Message = message;
        Link = link;
        CreatedAt = createdAt;
    }
    private NotificationItem()
    {

    }
    public static NotificationItem Create(
     UserId recipientUserId,
     NotificationType type,
     NotificationMessage message,
     DeepLink link,
      UserId? actorUserId = null
        )
     => new NotificationItem(
        NotificationId.Create(Guid.NewGuid()),
         actorUserId ?? UserId.System(),
         recipientUserId,
         type,
         message,
         link,
         DateTimeOffset.UtcNow
         );
    public static NotificationItem CreateWelcome(UserId recipientUserId)
    {
        return Create(
            recipientUserId,
            NotificationType.Welcome,
            NotificationMessage.Create("Welcom", "Welcome to our platform."),
            DeepLink.Create("/getting-started"),
            UserId.System());
    }
    public void MarkRead()
    {
        if (IsRead) return;
        ReadAt = DateTimeOffset.UtcNow;
    }

    public void MarkEmailSent()
    {
        if (IsEmailSent)
            return;

        EmailSentAt = DateTimeOffset.UtcNow;
    }
}
