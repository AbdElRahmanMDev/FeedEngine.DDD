using SocialGraph.Domain.Models;
using SocialGraph.Domain.ValueObjects;


namespace SocialGraph.Domain
{
    public interface IFollowRelationshipRepository
    {
        Task<bool> IsFollowingAsync(UserId followerId, UserId followingId);
        Task AddAsync(FollowRelationship followRelationship);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
