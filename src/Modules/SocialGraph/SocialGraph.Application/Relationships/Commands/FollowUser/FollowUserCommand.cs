using BuildingBlocks.Application.Messaging;
using MediatR;

namespace SocialGraph.Application.Relationships.Commands.FollowUser
{
    public record FollowUserCommand(Guid UserId) : ICommand<Unit>;

}
