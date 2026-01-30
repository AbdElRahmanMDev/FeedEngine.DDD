using BuildingBlocks.Application.Abstraction.Data;
using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Domain.Abstraction;
using Dapper;
using Identity.Application.User.DTOs;

namespace Identity.Application.User.Queries.GetUserByEmail;

internal sealed class GetUserByEmailQueryHandler : IQueryHandler<GetUserByEmailQuery, UserDto>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetUserByEmailQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<UserDto>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
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
            WHERE email = @Email AND status != 2
            """;

        var user = await connection.QueryFirstOrDefaultAsync<UserDto>(
            sql,
            new { Email = request.Email },
            commandTimeout: 5);

        if (user is null)
            return Result.Failure<UserDto>(new Error("User.NotFound", "User not found"));

        return Result.Success(user);
    }
}