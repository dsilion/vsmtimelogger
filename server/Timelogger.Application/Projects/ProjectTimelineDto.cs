using System;

namespace Timelogger.Application.Projects;

public class ProjectTimelineDto
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public DateTime StartDatetime { get; set; }
    public DateTime EndDatetime { get; set; }
}