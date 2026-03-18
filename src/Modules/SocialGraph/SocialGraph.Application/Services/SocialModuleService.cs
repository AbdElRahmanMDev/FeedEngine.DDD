using MediatR;
using SocialGraph.Application.Relationships.Queries.GetFollowers;
using SocialGraph.Contracts.Abstractions;
using SocialGraph.Domain.ValueObjects;
namespace SocialGraph.Application.Services
{
    public class SocialModuleService : ISocialModule
    {
        private readonly IMediator _mediator;
        public SocialModuleService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IReadOnlyList<Guid>> GetFollowersAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var vo = new UserId(userId);
            var followers = await _mediator.Send(new GetFollowersQuery(), cancellationToken);
            return followers.Value.Select(x => x.Value).ToList();
        }
    }

}
