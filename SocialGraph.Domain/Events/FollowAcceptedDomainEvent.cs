using BuildingBlocks.Domain.Abstraction;
using SocialGraph.Domain.ValueObjects;

namespace SocialGraph.Domain.Events
{
    public sealed record FollowAcceptedDomainEvent(UserId FollowerId, UserId FollowedId) : IDomainEvent;

}
