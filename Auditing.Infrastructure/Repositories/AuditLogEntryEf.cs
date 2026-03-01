using Auditing.Domain;
using Auditing.Infrastructure.Database;

namespace Auditing.Infrastructure.Repositories
{
    internal class AuditLogEntryEf : IAuditLogEntry
    {
        private readonly AuditingDbContext _auditingDbContext;
        public AuditLogEntryEf(AuditingDbContext auditingDbContext)
        {
            _auditingDbContext = auditingDbContext;
        }
        public async Task AddAsync(AuditLogEntry auditLogEntry, CancellationToken ct = default)
        {
            await _auditingDbContext.AuditEntries.AddAsync(auditLogEntry, ct);
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default) => await _auditingDbContext.SaveChangesAsync(ct);

    }
}
