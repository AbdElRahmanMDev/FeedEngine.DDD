using MediatR;

namespace Identity.Contracts.Events;

public record UserRegisteredIntegrationEvent(Guid EventId, Guid UserId, string Title, DateTime CreatedAtUtc) : INotification;

