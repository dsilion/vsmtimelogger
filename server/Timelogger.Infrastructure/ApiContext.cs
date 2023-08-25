using Microsoft.EntityFrameworkCore;
using Timelogger.Entities;

namespace Timelogger.Infrastructure;

public class ApiContext : DbContext, IApiContext
{
    public ApiContext(DbContextOptions<ApiContext> options)
        : base(options)
    {
    }

    public DbSet<Project> Projects { get; set; }

    public DbSet<ProjectTimeline> ProjectTimelines { get; set; }
}