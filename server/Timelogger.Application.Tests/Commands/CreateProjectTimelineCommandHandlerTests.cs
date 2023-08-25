using Moq;
using Timelogger.Application.Projects.Commands;
using Timelogger.Entities;

namespace Timelogger.Application.Tests.Commands;

public class CreateProjectTimelineCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidRequest_AddsTimelineAndReturnsId()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var startDate = DateTime.UtcNow;
        var endDate = startDate.AddDays(7);

        var mockRepository = new Mock<IRepository>();
        var project = new Project { Id = projectId, DueDate = DateTime.Now.AddDays(1)};
        mockRepository.Setup(r => r.GetByIdAsync(projectId, CancellationToken.None)).ReturnsAsync(project);

        var handler = new CreateProjectTimelineCommandHandler(mockRepository.Object);
        var request = new CreateProjectTimelineCommand
        { ProjectId = projectId, StartDate = startDate, EndDate = endDate };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(projectId, result);
        Assert.Single(project.ProjectTimelines); // Assuming there's a Timelines property in the Project class
        Assert.Equal(startDate, project.ProjectTimelines.First().StartDatetime);
        Assert.Equal(endDate, project.ProjectTimelines.First().EndDatetime);

        mockRepository.Verify(r => r.GetByIdAsync(projectId, CancellationToken.None), Times.Once);
        mockRepository.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}