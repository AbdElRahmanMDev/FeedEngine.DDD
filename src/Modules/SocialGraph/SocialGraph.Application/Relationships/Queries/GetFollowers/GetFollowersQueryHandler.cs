using BuildingBlocks.Application.Abstraction;
using BuildingBlocks.Application.Abstraction.Data;
using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Domain.Abstraction;
using Dapper;
using SocialGraph.Domain.Models.enums;
using SocialGraph.Domain.ValueObjects;

namespace SocialGraph.Application.Relationships.Queries.GetFollowers
{
    public sealed class GetFollowersQueryHandler : IQueryHandler<GetFollowersQuery, IReadOnlyList<UserId>>
    {
        private readonly ISqlConnectionFactory _db;
        private readonly ICurrentUserService _currentUserService;
        public GetFollowersQueryHandler(ISqlConnectionFactory db, ICurrentUserService currentUserService)
        {
            _db = db;
            _currentUserService = currentUserService;
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
                FollowedId = _currentUserService!.UserId!.Value,
                AcceptedStatus = (int)FollowStatus.Accepted
            };

            using var connection = _db.CreateConnection();

            var followerGuids = await connection.QueryAsync<Guid>(
                new CommandDefinition(sql, args, cancellationToken: cancellationToken));

            // If your UserId ctor/factory is different, adjust this line:
            // e.g. UserId.FromGuid(g) or new UserId(g)
            return followerGuids.Select(g => new UserId(g)).ToList();
        }
    }
}
