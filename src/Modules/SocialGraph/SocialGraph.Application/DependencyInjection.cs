using Microsoft.Extensions.DependencyInjection;

namespace SocialGraph.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddSocialGraphApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        config.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));
        return services;
    }
}
