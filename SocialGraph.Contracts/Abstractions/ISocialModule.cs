namespace SocialGraph.Contracts.Abstractions
{
    public interface ISocialModule
    {
        public Task<IReadOnlyList<Guid>> GetFollowersAsync(Guid userId, CancellationToken cancellationToken = default!);

    }
}
