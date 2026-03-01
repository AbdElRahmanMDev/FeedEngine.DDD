using Auditing.Domain;
using Identity.Contracts.Events;
using MediatR;

namespace Auditing.Application.Handlers
{
    internal class UserRegisteredIntegrationEventHandler : INotificationHandler<UserRegisteredIntegrationEvent>
    {
        private readonly IAuditLogEntry _auditLogEntry;
        public UserRegisteredIntegrationEventHandler(IAuditLogEntry auditLogEntry)
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
            await _auditLogEntry.AddAsync(entry);
            await _auditLogEntry.SaveChangesAsync(cancellationToken);

        }
    }
}
