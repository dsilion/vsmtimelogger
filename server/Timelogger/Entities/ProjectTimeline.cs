using System;
using Timelogger.Common;

namespace Timelogger.Entities;

public class ProjectTimeline
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public DateTime StartDatetime { get; set; }
    public DateTime EndDatetime { get; set; }
    public virtual Project Project { get; set; }

    public static ProjectTimeline Create(DateTime startDate, DateTime endDate)
    {
        if ((endDate - startDate).TotalMinutes < 30)
        {
            throw new BusinessException("Time spent must be minimum 30 minutes.");
        }

        return new ProjectTimeline {StartDatetime = startDate, EndDatetime = endDate};
    }
}