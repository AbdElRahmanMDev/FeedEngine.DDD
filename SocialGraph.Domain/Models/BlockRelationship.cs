using BuildingBlocks.Domain.Abstraction;
using SocialGraph.Domain.Events;
using SocialGraph.Domain.ValueObjects;

namespace SocialGraph.Domain.Models;

public sealed class BlockRelationship : Aggregate<RelationshipId>
{
    public UserId BlockerId { get; private set; }
    public UserId BlockedId { get; private set; }

    private BlockRelationship() { }

    private BlockRelationship(RelationshipId id, UserId blockerId, UserId blockedId)
    {
        if (blockerId == blockedId)
            throw new InvalidOperationException("User cannot block himself.");

        Id = id;
        BlockerId = blockerId;
        BlockedId = blockedId;

        Raise(new UserBlockedDomainEvent(blockerId, blockedId));
    }

    public static BlockRelationship Create(UserId blockerId, UserId blockedId)
        => new(RelationshipId.New(), blockerId, blockedId);
}
