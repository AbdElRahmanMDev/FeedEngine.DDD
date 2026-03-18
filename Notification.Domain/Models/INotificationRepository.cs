using Notification.Domain.ValueObjects;


namespace Notification.Domain.Models;

public interface INotificationRepository
{
    Task AddAsync(NotificationItem notification, CancellationToken ct);

    Task<NotificationItem?> GetForUserByIdAsync(
        UserId recipientUserId,
        NotificationId notificationId,
        CancellationToken ct);
    Task AddRange(IReadOnlyCollection<NotificationItem> notificationItems, CancellationToken cancellationToken = default);
    Task<int> MarkAllAsReadAsync(UserId recipientUserId, DateTimeOffset readAt, CancellationToken ct);
    Task<int> SaveChangesAsync(CancellationToken ct);
    Task<NotificationItem?> GetWelcomeByRecipientIdAsync(
    UserId recipientUserId,
    CancellationToken cancellationToken);

}
