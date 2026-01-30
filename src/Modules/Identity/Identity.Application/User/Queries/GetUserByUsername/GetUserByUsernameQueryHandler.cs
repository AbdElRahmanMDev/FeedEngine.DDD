using BuildingBlocks.Application.Abstraction.Data;
using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Domain.Abstraction;
using Dapper;
using Identity.Application.User.DTOs;

namespace Identity.Application.User.Queries.GetUserByUsername;

internal sealed class GetUserByUsernameQueryHandler : IQueryHandler<GetUserByUsernameQuery, UserDto>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetUserByUsernameQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<UserDto>> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
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
            WHERE username = @Username AND status != 2
            """;

        var user = await connection.QueryFirstOrDefaultAsync<UserDto>(
            sql,
            new { Username = request.Username },
            commandTimeout: 5);

        if (user is null)
            return Result.Failure<UserDto>(new Error("User.NotFound", "User not found"));

        return Result.Success(user);
    }
}