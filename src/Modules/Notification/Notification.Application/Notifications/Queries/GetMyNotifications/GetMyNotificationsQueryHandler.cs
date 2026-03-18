using BuildingBlocks.Application.Abstraction;
using BuildingBlocks.Application.Abstraction.Data;
using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Domain.Abstraction;
using Dapper;
using Notification.Application.Notifications.Queries.GetMyNotifications;
using System.Data;

namespace Notification.Application.Notification.Queries.GetNotifications
{
    internal sealed class GetMyNotificationsQueryHandler
         : IQueryHandler<GetMyNotificationsQuery, IReadOnlyList<NotificationResponse>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly ICurrentUserService _currentUserService;

        public GetMyNotificationsQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory,
            ICurrentUserService currentUserService)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _currentUserService = currentUserService;
        }

        public async Task<Result<IReadOnlyList<NotificationResponse>>> Handle(
      GetMyNotificationsQuery request,
      CancellationToken cancellationToken)
        {
            Guid currentUserId = _currentUserService!.UserId!.Value;

            const string sql = """
            SELECT
                ni.id AS Id,
                ni.actor_user_id AS ActorUserId,
                ni.type AS Type,
                ni.message_title AS Title,
                ni.message_body AS Body,
                ni.link AS Link,
                ni.created_at AS CreatedAt,
                ni.read_at AS ReadAt,
                ni.email_sent_at AS EmailSentAt
            FROM notification.notification_items ni
            WHERE ni.recipient_user_id = @RecipientUserId
            ORDER BY ni.created_at DESC
            """;

            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

            var command = new CommandDefinition(
                sql,
                new
                {
                    RecipientUserId = currentUserId
                },
                cancellationToken: cancellationToken);

            var notifications = await connection.QueryAsync<NotificationResponse>(command);

            IReadOnlyList<NotificationResponse> result = notifications.AsList();

            return Result.Success(result);
        }


    }
}
