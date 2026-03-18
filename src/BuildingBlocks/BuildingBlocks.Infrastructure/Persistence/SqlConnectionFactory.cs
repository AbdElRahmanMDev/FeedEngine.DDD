using BuildingBlocks.Application.Abstraction.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BuildingBlocks.Infrastructure.Persistence
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {

        private readonly string _connectionString;

        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);
    }
}
