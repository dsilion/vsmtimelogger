using System;
using System.Linq;
using System.Threading;
using Timelogger.Application.Common;
using Timelogger.Entities;
using Timelogger.Infrastructure;

namespace Timelogger.Api.Seed;

public static class ApiDbInitializer
{
    public static void Initialize(IApiContext context)
    {
        if (context.Projects.Any())
            return;

        context.Projects.Add(new Project
        {
            Name = "e-conomic Interview 1",
            DueDate = DateTime.Now.AddDays(10),
        });

        context.Projects.Add(new Project
        {
            Name = "e-conomic Interview 2",
            DueDate = DateTime.Now.AddDays(20),
        });

        context.Projects.Add(new Project
        {
            Name = "e-conomic Interview 3",
            DueDate = DateTime.Now.AddDays(30),
        });

        context.Projects.Add(new Project
        {
            Name = "e-conomic Interview 4 (closed)",
            DueDate = DateTime.Now.AddDays(-10),
        });

        context.SaveChangesAsync(new CancellationToken());
    }
}