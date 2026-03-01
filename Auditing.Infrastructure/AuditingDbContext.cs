using Microsoft.EntityFrameworkCore;


namespace Auditing.Infrastructure;

internal class AuditingDbContext : DbContext
{
    public AuditingDbContext(DbContextOptions<AuditingDbContext> options) : base(options)
    {

    }
}
