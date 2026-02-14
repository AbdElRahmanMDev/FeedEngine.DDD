using BuildingBlocks.Application.Abstraction;
using BuildingBlocks.Domain.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Content.Infrastructure.Database;

public class ContentDbContext : DbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;

    public ContentDbContext(DbContextOptions<ContentDbContext> options, IPublisher publisher)
        : base(options)
    {
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema.Content);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContentDbContext).Assembly);
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


