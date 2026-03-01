using Microsoft.Extensions.DependencyInjection;

namespace Auditing.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAuditingApplication(this IServiceCollection services)
        {
            services.AddMediatR(config =>
                        config.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));

            return services;
        }

    }
}
