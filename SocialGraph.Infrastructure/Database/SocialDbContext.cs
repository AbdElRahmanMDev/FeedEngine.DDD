using Microsoft.EntityFrameworkCore;
using SocialGraph.Domain.Models;

namespace SocialGraph.Infrastructure.Database
{
    internal class SocialDbContext : DbContext
    {
        DbSet<FollowRelationship> FollowRelationships { get; set; }
        public SocialDbContext(DbContextOptions<SocialDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema.Social);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SocialDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
