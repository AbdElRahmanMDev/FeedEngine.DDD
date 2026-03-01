using Auditing.Domain;
using Auditing.Infrastructure.Database;
using Auditing.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auditing.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAuditingInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var cs = configuration.GetConnectionString("AppDb") ??
            throw new InvalidOperationException("Connection string 'AppData' not found.");
            services.AddScoped<IAuditLogEntry, AuditLogEntryEf>();
            services.AddDbContext<AuditingDbContext>((option) =>
            {
                option.UseSqlServer(cs, sql =>
                {
                    sql.MigrationsAssembly(typeof(AuditingDbContext).Assembly.FullName);
                    sql.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schema.Auditing);


                });

            });

            return services;
        }
    }
}
