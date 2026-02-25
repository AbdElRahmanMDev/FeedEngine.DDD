using BuildingBlocks.Domain.Abstraction;
using SocialGraph.Domain.ValueObjects;

namespace SocialGraph.Domain.Events
{
    public record UserFollowedDomainEvent(UserId FollowerId, UserId FollowedId) : IDomainEvent;

}
