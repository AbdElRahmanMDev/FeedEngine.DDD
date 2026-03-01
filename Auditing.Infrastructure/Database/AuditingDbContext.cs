using Auditing.Domain;
using Microsoft.EntityFrameworkCore;


namespace Auditing.Infrastructure.Database;

public class AuditingDbContext : DbContext
{
    public AuditingDbContext(DbContextOptions<AuditingDbContext> options) : base(options)
    {

    }

    public DbSet<AuditLogEntry> AuditEntries => Set<AuditLogEntry>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema.Auditing);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuditingDbContext).Assembly);
        base.OnModelCreating(modelBuilder);

    }
}
