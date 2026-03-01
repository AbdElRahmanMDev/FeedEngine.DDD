using Identity.Application.Abstractions.Messaging;
using Identity.Contracts.Events;
using Identity.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Identity.Application.EventHandlers;

public class UserRegisteredDomainEventHandler : INotificationHandler<UserRegisteredDomainEvent>
{

    private readonly ILogger<UserRegisteredDomainEventHandler> _logger;
    private readonly IOutboxService _outboxService;

    public UserRegisteredDomainEventHandler(
        IOutboxService outboxService,
        ILogger<UserRegisteredDomainEventHandler> logger)
    {
        _logger = logger;
        _outboxService = outboxService;
    }

    public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new UserRegisteredIntegrationEvent(Guid.NewGuid(), notification.UserId.Value, "New User Registered", DateTime.UtcNow);

        _logger.LogInformation("User registered: {UserId}, {Email}", notification.UserId, notification.Email);

        await _outboxService.AddAsync(integrationEvent, cancellationToken);


    }
}
