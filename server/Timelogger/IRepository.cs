using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timelogger.Entities;

namespace Timelogger;

public interface IRepository
{
    Task<List<Project>> GetAllAsync(CancellationToken cancellationToken);
    Task<Project> GetByIdAsync(Guid projectId, CancellationToken cancellationToken);
    Task<Project> GetByIdWithTimelineAsync(Guid projectId, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}