using Identity.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace FeedEngine.DDD.API.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
            dbContext.Database.Migrate();
        }
    }
}
