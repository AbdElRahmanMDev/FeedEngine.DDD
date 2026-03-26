using Auditing.Abstractions.Models;
using Auditing.Abstractions.Persistence;
using Identity.Contracts.Events;
using MediatR;

namespace Auditing.Abstractions.Handlers
{
    internal class UserRegisteredIntegrationEventHandler : INotificationHandler<UserRegisteredIntegrationEvent>
    {
        private readonly AuditingDbContext _auditLogEntry;
        public UserRegisteredIntegrationEventHandler(AuditingDbContext auditLogEntry)
        {
            _auditLogEntry = auditLogEntry;

        }
        public async Task Handle(UserRegisteredIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var entry = new AuditLogEntry
            {
                Id = Guid.NewGuid(),
                Action = "UserRegistered",
                Data = $"UserId={notification.UserId}, CreatedAtUtc={notification.CreatedAtUtc:O}",
                CreatedAtUtc = DateTime.UtcNow
            };

            _auditLogEntry.AuditEntries.Add(entry);
            await _auditLogEntry.SaveChangesAsync(cancellationToken);
        }
    }
}
