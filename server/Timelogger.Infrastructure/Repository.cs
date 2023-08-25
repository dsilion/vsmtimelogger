using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Timelogger.Entities;

namespace Timelogger.Infrastructure;

public class Repository : IRepository
{
    private readonly IApiContext _context;

    public Repository(IApiContext context)
    {
        _context = context;
    }

    public async Task<List<Project>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Projects
            .AsNoTracking()
            .OrderBy(t => t.DueDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<Project> GetByIdAsync(Guid projectId, CancellationToken cancellationToken)
    {
        return await _context.Projects
            .Include(pt => pt.ProjectTimelines)
            .SingleOrDefaultAsync(pr => pr.Id == projectId, cancellationToken);
    }

    public async Task<Project> GetByIdWithTimelineAsync(Guid projectId, CancellationToken cancellationToken)
    {
        return await _context.Projects
            .AsNoTracking()
            .Include(p => p.ProjectTimelines)
            .SingleAsync(p => p.Id == projectId, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}