using Auditing.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Auditing.Abstractions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAuditingModule(this IServiceCollection services, IConfiguration configuration)
        {
            var cs = configuration.GetConnectionString("AppDb") ??
          throw new InvalidOperationException("Connection string 'AppData' not found.");
            services.AddDbContext<AuditingDbContext>((option) =>
            {
                option.UseSqlServer(cs, sql =>
                {
                    sql.MigrationsAssembly(typeof(AuditingDbContext).Assembly.FullName);
                    sql.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schema.Auditing);


                });

            });
            services.AddMediatR(config =>
                       config.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));

            return services;
        }
    }
}
