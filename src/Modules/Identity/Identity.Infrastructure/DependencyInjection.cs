using Identity.Domain;
using Identity.Infrastructure.Database;
using Identity.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var cs = configuration.GetConnectionString("AppDb") ??
            throw new InvalidOperationException("Connection string 'AppData' not found.");
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddDbContext<UsersDbContext>(option =>
        {
            option.UseSqlServer(cs, sql =>
            {
                sql.MigrationsAssembly(typeof(UsersDbContext).Assembly.FullName);
                sql.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schema.Users);
            });
        });
        return services;
    }
}
