using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application;

public static class DependcyInjection
{
    public static IServiceCollection AddIdentityApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(typeof(DependcyInjection).Assembly);
        });

        return services;
    }
}
