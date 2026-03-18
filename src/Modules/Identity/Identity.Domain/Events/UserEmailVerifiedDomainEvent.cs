using BuildingBlocks.Domain.Abstraction;
using Identity.Domain.ValueObjects;

namespace Identity.Domain.Events
{
    public record UserEmailVerifiedDomainEvent(UserId UserId, Email Email, DateTime nowUtc) : IDomainEvent;

}
