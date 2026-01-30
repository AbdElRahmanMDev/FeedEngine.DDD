using System.Data;

namespace BuildingBlocks.Application.Abstraction.Data;


public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}
