

namespace Auditing.Domain;

public interface IAuditLogEntry
{
    Task AddAsync(AuditLogEntry auditLogEntry, CancellationToken ct = default);
    Task<int> SaveChangesAsync(CancellationToken ct = default);

}
