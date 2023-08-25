using AutoMapper;
using Moq;
using Timelogger.Application.Projects;
using Timelogger.Application.Projects.Queries;
using Timelogger.Entities;

namespace Timelogger.Application.Tests.Queries;

public class GetProjectTimelinesQueryHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsProjectDtoWithTimelines()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var project = new Project
        {
            Id = projectId,
            Name = "Project 1",
            ProjectTimelines = new List<ProjectTimeline>
            {
                new() { Id = Guid.NewGuid(), StartDatetime = DateTime.UtcNow,EndDatetime = DateTime.UtcNow.AddDays(7) },
                new() { Id = Guid.NewGuid(), StartDatetime = DateTime.UtcNow, EndDatetime = DateTime.UtcNow.AddDays(14) }
            }
        };

        var projectDto = new ProjectDto
        {
            Id = project.Id,
            Name = "Project 1",
            ProjectTimelines = new List<ProjectTimelineDto>
            {
                new() { Id = project.ProjectTimelines[0].Id, StartDatetime = project.ProjectTimelines[0].StartDatetime, EndDatetime = project.ProjectTimelines[0].EndDatetime },
                new() { Id = project.ProjectTimelines[1].Id, StartDatetime = project.ProjectTimelines[1].StartDatetime, EndDatetime = project.ProjectTimelines[1].EndDatetime }
            }
        };

        var mockRepository = new Mock<IRepository>();
        mockRepository.Setup(r => r.GetByIdWithTimelineAsync(projectId, CancellationToken.None)).ReturnsAsync(project);

        var mockMapper = new Mock<IMapper>();
        mockMapper.Setup(m => m.Map<Project, ProjectDto>(project)).Returns(projectDto);

        var handler = new GetProjectTimelinesQueryHandler(mockRepository.Object, mockMapper.Object);
        var request = new GetProjectTimelinesQuery { ProjectId = projectId };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(projectDto.Id, result.Id);
        Assert.Equal(projectDto.ProjectTimelines.Count, result.ProjectTimelines.Count);
        Assert.Equal(projectDto.ProjectTimelines[0].Id, result.ProjectTimelines[0].Id);
        Assert.Equal(projectDto.ProjectTimelines[1].EndDatetime, result.ProjectTimelines[1].EndDatetime);

        mockRepository.Verify(r => r.GetByIdWithTimelineAsync(projectId, CancellationToken.None), Times.Once);
        mockMapper.Verify(m => m.Map<Project, ProjectDto>(project), Times.Once);
    }
}