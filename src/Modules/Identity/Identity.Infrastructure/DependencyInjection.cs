using BuildingBlocks.Application.Abstraction;
using BuildingBlocks.Application.Abstraction.Data;
using Identity.Application.Abstractions.Authentication;
using Identity.Application.Abstractions.Security;
using Identity.Domain;
using Identity.Infrastructure.Auth;
using Identity.Infrastructure.Database;
using Identity.Infrastructure.Database.Connections;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UsersDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ =>
            new SqlConnectionFactory(configuration.GetConnectionString("AppDb")!));

        services.AddOptions<JwtOptions>()
       .Bind(configuration.GetSection("Jwt"))
       .Validate(o => !string.IsNullOrWhiteSpace(o.SecretKey), "Jwt:SecretKey is missing")
       .ValidateOnStart();

        services.AddSingleton<IJwtProvider, JwtProvider>();

        var jwtOptions = configuration.GetSection("Jwt").Get<JwtOptions>()
            ?? throw new InvalidOperationException("Jwt section is missing.");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateAudience = true,

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
            };
        });

        services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
        return services;
    }
}
