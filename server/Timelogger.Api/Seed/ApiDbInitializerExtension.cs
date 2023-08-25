using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Timelogger.Application.Common;
using Timelogger.Infrastructure;

namespace Timelogger.Api.Seed;

public static class ApiDbInitializerExtension
{
    public static IApplicationBuilder SeedDatabase(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));

        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<IApiContext>();
        ApiDbInitializer.Initialize(context);

        return app;
    }
}