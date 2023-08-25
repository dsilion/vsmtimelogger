using System;
using System.Collections.Generic;

namespace Timelogger.Application.Projects;

public class ProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime DueDate { get; internal set; }
    public IList<ProjectTimelineDto> ProjectTimelines { get; set; }
}