using BuildingBlocks.Domain.Abstraction;
using SocialGraph.Domain.Events;
using SocialGraph.Domain.Models.enums;
using SocialGraph.Domain.ValueObjects;


namespace SocialGraph.Domain.Models;

public sealed class FollowRelationship : Aggregate<RelationshipId>
{
    public UserId FollowerId { get; private set; }
    public UserId FollowedId { get; private set; }
    public FollowStatus Status { get; private set; }

    private FollowRelationship() { }

    private FollowRelationship(RelationshipId id, UserId followerId, UserId followedId, bool isPrivateAccount)
    {
        if (followerId == followedId)
            throw new InvalidOperationException("User cannot follow himself.");

        Id = id;
        FollowerId = followerId;
        FollowedId = followedId;
        Status = isPrivateAccount ? FollowStatus.Pending : FollowStatus.Accepted;

        Raise(new UserFollowedDomainEvent(followerId, followedId));
    }

    public static FollowRelationship Create(UserId followerId, UserId followedId, bool isPrivateAccount)
        => new(RelationshipId.New(), followerId, followedId, isPrivateAccount);

    public void Accept()
    {
        if (Status != FollowStatus.Pending)
            throw new InvalidOperationException("Only pending follow requests can be accepted.");

        Status = FollowStatus.Accepted;

        Raise(new FollowAcceptedDomainEvent(FollowerId, FollowedId));
    }

    public void Reject()
    {
        if (Status != FollowStatus.Pending)
            throw new InvalidOperationException("Only pending follow requests can be rejected.");

        Status = FollowStatus.Rejected;
    }

    public void Unfollow()
    {
        if (Status == FollowStatus.Rejected)
            throw new InvalidOperationException("Cannot unfollow a rejected relationship.");

        Status = FollowStatus.Rejected;
    }
}
