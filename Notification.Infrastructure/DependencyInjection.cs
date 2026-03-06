using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.Domain.Models;
using Notification.Infrastructure.Database;
using Notification.Infrastructure.Repositories;

namespace Notification.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddNotificationInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var cs = configuration.GetConnectionString("AppDb") ??
           throw new InvalidOperationException("Connection string 'AppData' not found.");
            services.AddDbContext<NotificationDbContext>((option) =>
            {
                option.UseSqlServer(cs, sql =>
                {
                    sql.MigrationsAssembly(typeof(NotificationDbContext).Assembly.FullName);
                    sql.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schema.Notification);

                });

            });
            services.AddScoped<INotificationRepository, NotificationRepository>();
            return services;
        }
    }
}
