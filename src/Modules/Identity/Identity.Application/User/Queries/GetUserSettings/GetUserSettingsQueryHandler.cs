using BuildingBlocks.Application.Abstraction.Data;
using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Domain.Abstraction;
using Dapper;
using Identity.Application.Abstractions.Authentication;
using Identity.Application.User.DTOs;
using Identity.Domain.Models.enums;

namespace Identity.Application.User.Queries.GetUserSettings
{
    public sealed class GetUserSettingsQueryHandler
    : IQueryHandler<GetUserSettingsQuery, UserSettingsDto>
    {
        private readonly ICurrentUserService _currentUser;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUserSettingsQueryHandler(ICurrentUserService currentUser, ISqlConnectionFactory sqlConnectionFactory)
        {
            _currentUser = currentUser;
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<Result<UserSettingsDto>> Handle(GetUserSettingsQuery request, CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();

            const string sql = """
            SELECT
                Theme,
                Language,
                NotificationsEnabled,
                PrivacyLevel
            FROM Users
            WHERE Id = @UserId
            """;
            var userId = _currentUser.UserId;
            var row = await connection.QuerySingleOrDefaultAsync<UserSettingsRow>(
          new CommandDefinition(sql, new { UserId = userId }, cancellationToken: cancellationToken));

            if (row is null)
                return Result.Failure<UserSettingsDto>(
                    new Error("User.NotFound", "User not found"));

            var theme = Enum.IsDefined(typeof(ThemeMode), row.Theme)
                ? (ThemeMode)row.Theme
                : ThemeMode.Light;

            var privacy = Enum.IsDefined(typeof(PrivacyLevel), row.PrivacyLevel)
                ? (PrivacyLevel)row.PrivacyLevel
                : PrivacyLevel.Public;

            var dto = new UserSettingsDto(
                theme,
                row.Language,
                row.NotificationsEnabled,
                privacy);

            return Result.Success(dto);
        }
        private sealed record UserSettingsRow(
          int Theme,
          string Language,
          bool NotificationsEnabled,
          int PrivacyLevel);
    }

}


