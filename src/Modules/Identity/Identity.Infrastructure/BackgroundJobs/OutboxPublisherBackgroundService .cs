using Identity.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace Identity.Infrastructure.BackgroundJobs
{
    public sealed class OutboxPublisherBackgroundService : BackgroundService
    {
        private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

        private readonly IServiceProvider _serviceProvider;

        public OutboxPublisherBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                var messages = await dbContext.OutboxMessages
                    .Where(x => x.ProcessedOnUtc == null)
                    .OrderBy(x => x.OccurredOnUtc)
                    .Take(20)
                    .ToListAsync(stoppingToken);

                foreach (var message in messages)
                {
                    try
                    {
                        var type = Type.GetType(message.Type);
                        if (type is null)
                            throw new InvalidOperationException($"Cannot resolve type '{message.Type}'.");

                        var integrationEvent =
                            (INotification?)JsonSerializer.Deserialize(message.Content, type, JsonOptions);

                        if (integrationEvent is null)
                            throw new InvalidOperationException($"Cannot deserialize event '{message.Type}'.");

                        await mediator.Publish(integrationEvent, stoppingToken);

                        message.ProcessedOnUtc = DateTime.UtcNow;
                    }
                    catch (Exception ex)
                    {
                        message.Error = ex.ToString();
                        message.ProcessedOnUtc = DateTime.UtcNow; // mark as processed to avoid infinite retry
                    }
                }

                await dbContext.SaveChangesAsync(stoppingToken);

                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            }
        }
    }
}
