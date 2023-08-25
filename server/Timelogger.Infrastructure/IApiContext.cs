using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timelogger.Entities;

namespace Timelogger.Infrastructure
{
    public interface IApiContext
    {
        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectTimeline> ProjectTimelines { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}