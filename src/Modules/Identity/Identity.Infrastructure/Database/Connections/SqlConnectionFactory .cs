using BuildingBlocks.Application.Abstraction.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Identity.Infrastructure.Database.Connections;

public sealed class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);
}
