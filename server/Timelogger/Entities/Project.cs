using System;
using System.Collections.Generic;
using Timelogger.Common;

namespace Timelogger.Entities;

public class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime DueDate { get; set; }
    public virtual IList<ProjectTimeline> ProjectTimelines { get; set; }
    public void AddTimeline(DateTime startDate, DateTime endDate)
    {
        if (DueDate < DateTime.Now)
        {
            throw new BusinessException("The project is complete it can no longer receive new registrations.");
        }

        if (ProjectTimelines == null)
        {
            ProjectTimelines = new List<ProjectTimeline>();
        }

        ProjectTimelines.Add(ProjectTimeline.Create(startDate, endDate));
    }
}