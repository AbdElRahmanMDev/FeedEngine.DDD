using BuildingBlocks.Domain.Abstraction;
using SocialGraph.Domain.Events;
using SocialGraph.Domain.ValueObjects;
namespace SocialGraph.Domain.Models;

public sealed class FollowRelationship : Aggregate<RelationshipId>
{
    public UserId FollowerId { get; private set; }
    public UserId FollowedId { get; private set; }

    private FollowRelationship() { }

    private FollowRelationship(RelationshipId id, UserId followerId, UserId followedId)
    {
        if (followerId == followedId)
            throw new InvalidOperationException("User cannot follow himself.");

        Id = id;
        FollowerId = followerId;
        FollowedId = followedId;

        Raise(new UserFollowedDomainEvent(followerId, followedId));
    }

    public static FollowRelationship Create(UserId followerId, UserId followedId)
        => new(RelationshipId.New(), followerId, followedId);


}
