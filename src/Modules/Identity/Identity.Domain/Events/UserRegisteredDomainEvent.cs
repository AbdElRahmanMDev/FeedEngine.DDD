using BuildingBlocks.Domain.Abstraction;
using Identity.Domain.ValueObjects;
namespace Identity.Domain.Events;

public sealed record UserRegisteredDomainEvent(
UserId UserId,
Email Email,
Username Username,
DateTime OccurredOnUtc
) : IDomainEvent;
