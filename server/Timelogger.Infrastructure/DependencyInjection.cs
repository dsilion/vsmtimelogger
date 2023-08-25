using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Timelogger.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<ApiContext>(opt =>
        {
            opt.UseInMemoryDatabase("e-conomic interview");
        });
        services.AddScoped<IApiContext>(provider => provider.GetRequiredService<ApiContext>());

        services.AddScoped<IApiContext>(provider => provider.GetRequiredService<ApiContext>());

        services.AddScoped<IRepository, Repository>();

        return services;
    }
}