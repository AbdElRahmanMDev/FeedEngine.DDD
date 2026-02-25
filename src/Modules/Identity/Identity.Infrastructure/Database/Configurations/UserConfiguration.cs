using Identity.Domain.Models;
using Identity.Domain.Models.enums;
using Identity.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Database.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(
                    id => id.Value,
                    value => UserId.Of(value))
                .ValueGeneratedNever();

            builder.Property(x => x.Email)
                .HasMaxLength(100)
                .HasConversion(x => x.Value, x => Email.Create(x));

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(20)
            .HasConversion(
                username => username.Value,
                value => Username.Create(value));

            builder.HasIndex(x => x.Username)
                .IsUnique();

            builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(500)
            .HasConversion(
                hash => hash.Value,
                value => PasswordHash.FromHash(value));

            builder.ToTable("Users", t =>
            {
                t.HasCheckConstraint(
                    "CK_Users_Status",
                    "[Status] IN (1, 2, 3)" // Active=1, Suspended=2, Deleted=3
                );
            });

            builder.Property(u => u.Status)
                   .HasConversion<int>()
                   .IsRequired();

            builder.OwnsOne(u => u.Settings, settings =>
            {
                settings.Property(x => x.Theme)
                    .HasColumnName("Theme")
                    .HasConversion<int>()
                    .IsRequired()
                    .HasDefaultValue(ThemeMode.Light);

                settings.Property(x => x.Language)
                    .HasColumnName("Language")
                    .HasMaxLength(10)
                    .IsRequired()
                    .HasDefaultValue("en");

                settings.Property(x => x.NotificationsEnabled)
                    .HasColumnName("NotificationsEnabled")
                    .IsRequired()
                    .HasDefaultValue(true);

                settings.Property(x => x.PrivacyLevel)
                    .HasColumnName("PrivacyLevel")
                    .HasConversion<int>()
                    .IsRequired()
                    .HasDefaultValue(PrivacyLevel.Public);
            });

            builder.Navigation(u => u.Settings).IsRequired();

            // Optional: default value at DB level
            builder.Property(u => u.Status)
                   .HasDefaultValue(AccountStatus.Active);
        }
    }
}
