using BuildingBlocks.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Database.Configurations
{
    public sealed class OutboxMessageEntityTypeConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.OccurredOnUtc).IsRequired();
            builder.Property(x => x.Type).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.Content).IsRequired();

            builder.Property(x => x.ProcessedOnUtc);
            builder.Property(x => x.Error);
        }
    }
}
