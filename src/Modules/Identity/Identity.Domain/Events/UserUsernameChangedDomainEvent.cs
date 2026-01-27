using BuildingBlocks.Domain.Abstraction;
using Identity.Domain.ValueObjects;

namespace Identity.Domain.Events
{
    public record UserUsernameChangedDomainEvent(UserId UserId, Username old, Username next, DateTime nowUtc) : IDomainEvent;

}
