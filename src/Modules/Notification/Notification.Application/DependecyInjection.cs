using Microsoft.Extensions.DependencyInjection;


namespace Notification.Application;

public static class DependecyInjection
{
    public static IServiceCollection AddNotificationApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>

            config.RegisterServicesFromAssemblyContaining(typeof(DependecyInjection)));



        return services;
    }
}
