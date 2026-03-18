using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialGraph.Infrastructure.Database;

namespace SocialGraph.Infrastructure
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructureSocial(this IServiceCollection services, IConfiguration configuration)
        {
            var cs = configuration.GetConnectionString("AppDb") ??
         throw new InvalidOperationException("Connection string 'AppData' not found.");



            services.AddDbContext<SocialDbContext>((option) =>
            {
                option.UseSqlServer(cs, sql =>
                {
                    sql.MigrationsAssembly(typeof(SocialDbContext).Assembly.FullName);
                    sql.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schema.Social);

                });

            });
            return services;
        }
    }
}
