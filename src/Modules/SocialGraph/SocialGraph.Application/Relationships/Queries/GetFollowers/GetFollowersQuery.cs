using BuildingBlocks.Application.Messaging;
using SocialGraph.Domain.ValueObjects;

namespace SocialGraph.Application.Relationships.Queries.GetFollowers;

public sealed record GetFollowersQuery() : IQuery<IReadOnlyList<UserId>>;

