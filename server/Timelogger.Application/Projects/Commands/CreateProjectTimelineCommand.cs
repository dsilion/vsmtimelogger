using System;
using MediatR;

namespace Timelogger.Application.Projects.Commands;

public record CreateProjectTimelineCommand : IRequest<Guid>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid ProjectId { get; set; }
}