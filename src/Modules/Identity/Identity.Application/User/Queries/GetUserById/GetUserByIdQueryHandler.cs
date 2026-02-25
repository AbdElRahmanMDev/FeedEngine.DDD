
using Dapper;
using Identity.Application.User.DTOs;

namespace Identity.Application.User.Queries.GetUserById;

internal sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDto>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly ICurrentUserService _currentUserService;
    public GetUserByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory, ICurrentUserService currentUserService)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _currentUserService = currentUserService;
    }

    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();
        var userId = _currentUserService.UserId;
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
            new { UserId = userId },
            commandTimeout: 5);

        if (user is null)
            return Result.Failure<UserDto>(new Error("User.NotFound", "User not found"));

        return Result.Success(user);
    }
}