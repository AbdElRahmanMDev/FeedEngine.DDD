using System.Data;

namespace SocialGraph.Application.Abstractions;

public interface IDbConnectionFactory
{
    Task<IDbConnection> OpenConnectionAsync(CancellationToken cancellationToken = default);

}
