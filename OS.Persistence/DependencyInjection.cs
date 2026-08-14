using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OS.Application.interfaces;

namespace OS.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["OSDatabase"];
            services.AddDbContext<OSDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddScoped<IOSDbContext>(provider => provider.GetRequiredService<OSDbContext>());
            return services;
        }
    }
}
