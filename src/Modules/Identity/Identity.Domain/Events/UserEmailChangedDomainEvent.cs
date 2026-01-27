using BuildingBlocks.Domain.Abstraction;
using Identity.Domain.ValueObjects;

namespace Identity.Domain.Events
{
    public record UserEmailChangedDomainEvent(UserId UserId, Email old, Email next, DateTime Timestamp) : IDomainEvent;

}
