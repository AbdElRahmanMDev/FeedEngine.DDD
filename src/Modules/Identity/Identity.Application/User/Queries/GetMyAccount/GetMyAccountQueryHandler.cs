using BuildingBlocks.Application.Abstraction.Data;
using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Domain.Abstraction;
using Dapper;
using Identity.Application.User.DTOs;

namespace Identity.Application.User.Queries.GetMyAccount;

internal sealed class GetMyAccountQueryHandler : IQueryHandler<GetMyAccountQuery, UserAccountDto>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetMyAccountQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<UserAccountDto>> Handle(GetMyAccountQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT 
                id AS UserId,
                email AS Email,
                username AS Username,
                email_verified AS EmailVerified,
                status AS Status,
                created_at AS CreatedAt,
                updated_at AS UpdatedAt
            FROM users.Users
            WHERE id = @UserId AND status != 2
            """;

        var user = await connection.QueryFirstOrDefaultAsync<UserAccountDto>(
            sql,
            new { UserId = request.UserId },
            commandTimeout: 5);

        if (user is null)
            return Result.Failure<UserAccountDto>(new Error("User.NotFound", "User not found"));

        return Result.Success(user);
    }
}