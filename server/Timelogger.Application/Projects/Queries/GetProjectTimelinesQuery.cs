using System;
using MediatR;

namespace Timelogger.Application.Projects.Queries;

public class GetProjectTimelinesQuery : IRequest<ProjectDto>
{
    public Guid ProjectId { get; set; }
}