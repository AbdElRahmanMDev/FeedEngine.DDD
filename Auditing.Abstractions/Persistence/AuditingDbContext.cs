using Auditing.Abstractions.Models;
using Microsoft.EntityFrameworkCore;


namespace Auditing.Abstractions.Persistence
{
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
}
