using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialGraph.Infrastructure.Connections;

namespace SocialGraph.Infrastructure
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructureSocial(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IDbConnectionFactory>(_ =>
                new SqlConnectionFactory(configuration.GetConnectionString("AppDb")!));

            return services;
        }
    }
}
