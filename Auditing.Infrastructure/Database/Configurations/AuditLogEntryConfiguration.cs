using Auditing.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditing.Infrastructure.Database.Configurations
{
    internal class AuditLogEntryConfiguration : IEntityTypeConfiguration<AuditLogEntry>
    {
        public void Configure(EntityTypeBuilder<AuditLogEntry> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Action).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Data).IsRequired();
            builder.Property(x => x.CreatedAtUtc).IsRequired();
        }
    }
}
