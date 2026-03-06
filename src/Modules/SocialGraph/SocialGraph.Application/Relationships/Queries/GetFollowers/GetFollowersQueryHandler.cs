using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Domain.Abstraction;
using Dapper;
using SocialGraph.Application.Abstractions;
using SocialGraph.Domain.Models.enums;
using SocialGraph.Domain.ValueObjects;

namespace SocialGraph.Application.Relationships.Queries.GetFollowers
{
    public sealed class GetFollowersQueryHandler : IQueryHandler<GetFollowersQuery, IReadOnlyList<UserId>>
    {
        private readonly IDbConnectionFactory _db;
        public GetFollowersQueryHandler(IDbConnectionFactory db)
        {
            _db = db;
        }
        public async Task<Result<IReadOnlyList<UserId>>> Handle(GetFollowersQuery request, CancellationToken cancellationToken)
        {
            const string sql = """
                SELECT FollowerId
                FROM FollowRelationships
                WHERE FollowedId = @FollowedId
                  AND Status = @AcceptedStatus;
                """;

            // Assumption: FollowerId/FollowedId stored as uniqueidentifier (Guid) in DB.
            // Assumption: UserId has a Guid value property like request.UserId.Value
            var args = new
            {
                FollowedId = request.UserId.Value,
                AcceptedStatus = (int)FollowStatus.Accepted
            };

            using var connection = await _db.OpenConnectionAsync(cancellationToken);

            var followerGuids = await connection.QueryAsync<Guid>(
                new CommandDefinition(sql, args, cancellationToken: cancellationToken));

            // If your UserId ctor/factory is different, adjust this line:
            // e.g. UserId.FromGuid(g) or new UserId(g)
            return followerGuids.Select(g => new UserId(g)).ToList();
        }
    }
}
