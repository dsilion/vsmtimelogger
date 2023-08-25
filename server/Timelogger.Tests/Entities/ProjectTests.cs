using Timelogger.Common;
using Timelogger.Entities;

namespace Timelogger.Tests.Entities;

public class ProjectTests
{
    [Fact]
    public void AddTimeline_ValidDates_TimelineAdded()
    {
        // Arrange
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = "Test Project",
            DueDate = DateTime.Now.AddDays(10), // Assuming the due date is in the future
            ProjectTimelines = new List<ProjectTimeline>()
        };

        var startDate = DateTime.Now.AddDays(1);
        var endDate = DateTime.Now.AddDays(3);

        // Act
        project.AddTimeline(startDate, endDate);

        // Assert
        Assert.Equal(1, project.ProjectTimelines.Count);
        Assert.Equal(startDate, project.ProjectTimelines[0].StartDatetime);
        Assert.Equal(endDate, project.ProjectTimelines[0].EndDatetime);
    }

    [Fact]
    public void AddTimeline_PastDueDate_ThrowsBusinessException()
    {
        // Arrange
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = "Test Project",
            DueDate = DateTime.Now.AddDays(-1), // Assuming the due date is in the past
            ProjectTimelines = new List<ProjectTimeline>()
        };

        var startDate = DateTime.Now.AddDays(1);
        var endDate = DateTime.Now.AddDays(3);

        // Act & Assert
        var exception = Assert.Throws<BusinessException>(() => project.AddTimeline(startDate, endDate));
        Assert.Equal("The project is complete it can no longer receive new registrations.", exception.Message);
    }

    [Fact]
    public void AddTimeline_NullProjectTimelines_CreatesProjectTimelinesList()
    {
        // Arrange
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = "Test Project",
            DueDate = DateTime.Now.AddDays(10), // Assuming the due date is in the future
            ProjectTimelines = null
        };

        var startDate = DateTime.Now.AddDays(1);
        var endDate = DateTime.Now.AddDays(3);

        // Act
        project.AddTimeline(startDate, endDate);

        // Assert
        Assert.NotNull(project.ProjectTimelines);
        Assert.Equal(1, project.ProjectTimelines.Count);
        Assert.Equal(startDate, project.ProjectTimelines[0].StartDatetime);
        Assert.Equal(endDate, project.ProjectTimelines[0].EndDatetime);
    }
}