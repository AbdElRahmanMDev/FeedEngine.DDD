using Microsoft.Extensions.DependencyInjection;
using SocialGraph.Application.Services;
using SocialGraph.Contracts.Abstractions;

namespace SocialGraph.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddSocialGraphApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        config.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));
        services.AddScoped<ISocialModule, SocialModuleService>();

        return services;
    }
}
