using BuildingBlocks.Domain.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Identity.Infrastructure.Database
{
    public class UsersDbContext : DbContext
    {
        private readonly IPublisher _publisher;

        public UsersDbContext(DbContextOptions<UsersDbContext> options, IPublisher publisher)
            : base(options)
        {
            _publisher = publisher;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema.Users);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsersDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            var result = await base.SaveChangesAsync(cancellationToken);

            await PublishDomainEvents();

            return result;

        }


        private async Task PublishDomainEvents()
        {
            var domainEvents = ChangeTracker
              .Entries()
              .Where(e => e.Entity is IAggregate) // marker interface
              .Select(e => e.Entity as IAggregate)
              .SelectMany(aggregate =>
              {
                  var events = aggregate.DomainEvents.ToArray();
                  aggregate.ClearDomainEvents();
                  return events;
              })
              .ToList();

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, CancellationToken.None);
            }
        }
    }
}
