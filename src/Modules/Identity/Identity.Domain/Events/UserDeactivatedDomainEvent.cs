
using BuildingBlocks.Domain.Abstraction;
using Identity.Domain.ValueObjects;

namespace Identity.Domain.Events;

public record UserDeactivatedDomainEvent(UserId UserId, DateTime Timestamp) : IDomainEvent;

