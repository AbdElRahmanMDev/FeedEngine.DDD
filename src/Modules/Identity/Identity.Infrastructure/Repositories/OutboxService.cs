using BuildingBlocks.Infrastructure.Outbox;
using Identity.Application.Abstractions.Messaging;
using Identity.Infrastructure.Database;
using MediatR;
using System.Text.Json;

namespace Identity.Infrastructure.Repositories
{
    internal class OutboxService : IOutboxService
    {
        private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
        private readonly UsersDbContext _usersDbContext;

        public OutboxService(UsersDbContext usersDbContext)
        {
            _usersDbContext = usersDbContext;
        }
        public async Task AddAsync(INotification integrationEvent, CancellationToken cancellationToken)
        {
            var type = integrationEvent.GetType();

            var message = new OutboxMessage
            {
                Id = Guid.NewGuid(),
                OccurredOnUtc = DateTime.UtcNow,
                Type = type.AssemblyQualifiedName
                   ?? throw new InvalidOperationException("Event type must have AssemblyQualifiedName."),
                Content = JsonSerializer.Serialize(integrationEvent, type, JsonOptions),
                ProcessedOnUtc = null,
                Error = null
            };
            await _usersDbContext.OutboxMessages.AddAsync(message);

        }
    }
}
