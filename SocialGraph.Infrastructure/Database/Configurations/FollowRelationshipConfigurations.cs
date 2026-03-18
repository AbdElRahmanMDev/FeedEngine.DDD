using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialGraph.Domain.Models;
using SocialGraph.Domain.ValueObjects;

namespace SocialGraph.Infrastructure.Database.Configurations
{
    public class FollowRelationshipConfigurations : IEntityTypeConfiguration<FollowRelationship>
    {
        public void Configure(EntityTypeBuilder<FollowRelationship> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => new RelationshipId(value));

            builder.Property(x => x.FollowerId)
                .IsRequired()
                .HasConversion(
                    id => id.Value,
                    value => new UserId(value));

            builder.Property(x => x.FollowedId)
                .IsRequired()
                .HasConversion(
                    id => id.Value,
                    value => new UserId(value));


            builder.HasIndex(x => new { x.FollowerId, x.FollowedId })
                .IsUnique();

            builder.ToTable(t =>
                t.HasCheckConstraint(
                    "CK_FollowRelationships_FollowerId_FollowedId",
                    "[FollowerId] <> [FollowedId]"));
        }
    }
}
