using BuildingBlocks.Application.Messaging;
using SocialGraph.Domain.ValueObjects;

namespace SocialGraph.Application.Relationships.Queries.GetFollowers;

public sealed record GetFollowersQuery(UserId UserId) : IQuery<IReadOnlyList<UserId>>;

