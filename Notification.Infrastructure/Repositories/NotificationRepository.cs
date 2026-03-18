using Microsoft.EntityFrameworkCore;
using Notification.Domain.Models;
using Notification.Domain.Models.enums;
using Notification.Domain.ValueObjects;
using Notification.Infrastructure.Database;

namespace Notification.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly NotificationDbContext _db;

        public NotificationRepository(NotificationDbContext db)
        {
            _db = db;
        }
        public Task AddAsync(Domain.Models.NotificationItem notification, CancellationToken ct)
        {
            _db.Notifications.Add(notification);
            return Task.CompletedTask;
        }

        public async Task AddRange(IReadOnlyCollection<NotificationItem> notificationItems, CancellationToken cancellationToken = default)
        {
            await _db.Notifications.AddRangeAsync(notificationItems, cancellationToken);
        }
        public async Task<NotificationItem?> GetWelcomeByRecipientIdAsync(
            UserId recipientUserId,
            CancellationToken cancellationToken)
        {
            return await _db.Notifications
                .FirstOrDefaultAsync(
                    x => x.RecipientUserId == recipientUserId &&
                         x.Type == NotificationType.Welcome,
                    cancellationToken);
        }

        public Task<Domain.Models.NotificationItem?> GetForUserByIdAsync(UserId recipientUserId, NotificationId notificationId, CancellationToken ct)
        {
            return _db.Notifications
                       .FirstOrDefaultAsync(x => x.Id == notificationId && x.RecipientUserId == recipientUserId, ct);
        }

        public async Task<int> MarkAllAsReadAsync(UserId recipientUserId, DateTimeOffset readAt, CancellationToken ct)
        {
            try
            {
                return await _db.Notifications
                    .Where(x => x.RecipientUserId == recipientUserId && x.ReadAt == null)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.ReadAt, readAt), ct);
            }
            catch (MissingMethodException)
            {
                // Fallback for older EF: load + update
                var unread = await _db.Notifications
                    .Where(x => x.RecipientUserId == recipientUserId && x.ReadAt == null)
                    .ToListAsync(ct);

                foreach (var n in unread)
                    n.MarkRead();

                return unread.Count;
            }
        }

        public Task<int> SaveChangesAsync(CancellationToken ct)
        {
            return _db.SaveChangesAsync(ct);
        }
    }
}
