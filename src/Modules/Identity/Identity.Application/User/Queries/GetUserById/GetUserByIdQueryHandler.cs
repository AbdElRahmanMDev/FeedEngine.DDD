using BuildingBlocks.Application.Abstraction.Data;
using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Domain.Abstraction;
using Dapper;
using Identity.Application.User.DTOs;

namespace Identity.Application.User.Queries.GetUserById;

internal sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDto>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetUserByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
                SELECT 
                    [Id]            AS UserId,
                    [Email]         AS Email,
                    [Username]      AS Username,
                    [EmailVerified] AS EmailVerified,
                    [Status]        AS Status,
                    [CreatedAt]     AS CreatedAt,
                    [LastModified]  AS UpdatedAt
                FROM [users].[Users]
                WHERE [Id] = @UserId AND [Status] <> 2;
                """;


        var user = await connection.QueryFirstOrDefaultAsync<UserDto>(
            sql,
            new { UserId = request.UserId },
            commandTimeout: 5);

        if (user is null)
            return Result.Failure<UserDto>(new Error("User.NotFound", "User not found"));

        return Result.Success(user);
    }
}