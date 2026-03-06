
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notification.Domain.ValueObjects;

namespace Notification.Infrastructure.Database.Configurations;

public class NotificationConfigurations : IEntityTypeConfiguration<Notification.Domain.Models.NotificationItem>
{
    public void Configure(EntityTypeBuilder<Domain.Models.NotificationItem> b)
    {
        b.ToTable("Notifications");

        b.HasKey(x => x.Id);

        b.Property(x => x.Id)
            .HasConversion(v => v.Value, v => new NotificationId(v))
            .ValueGeneratedNever();

        b.Property(x => x.RecipientUserId)
            .HasConversion(v => v.Value, v => new UserId(v))
            .IsRequired();

        b.Property(x => x.Type)
            .IsRequired();

        b.Property(x => x.CreatedAt)
            .IsRequired();

        b.Property(x => x.ReadAt);

        b.OwnsOne(x => x.Message, mb =>
        {
            mb.Property(p => p.Title)
                .HasMaxLength(120)
                .IsRequired();

            mb.Property(p => p.Body)
                .HasMaxLength(500)
                .IsRequired();
        });

        b.OwnsOne(x => x.Link, lb =>
        {
            lb.Property(p => p.Value)
                .HasColumnName("DeepLink")
                .HasMaxLength(2048)
                .IsRequired();
        });

        b.HasIndex(x => new { x.RecipientUserId, x.ReadAt });
        b.HasIndex(x => x.RecipientUserId);


    }
}
