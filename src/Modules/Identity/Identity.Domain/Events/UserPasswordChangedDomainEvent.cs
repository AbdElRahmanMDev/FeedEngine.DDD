using BuildingBlocks.Domain.Abstraction;
using Identity.Domain.ValueObjects;

namespace Identity.Domain.Events
{
    public record UserPasswordChangedDomainEvent(UserId UserId, DateTime nowUtc) : IDomainEvent;

}
