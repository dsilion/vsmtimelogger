using AutoMapper;
using Moq;
using Timelogger.Application.Projects;
using Timelogger.Application.Projects.Queries;
using Timelogger.Entities;

namespace Timelogger.Application.Tests.Queries;

public class GetProjectsQueryHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsListOfProjectDtos()
    {
        // Arrange
        var projects = new List<Project>
        {
            new Project { Id = Guid.NewGuid(), Name = "Project 1" },
            new Project { Id = Guid.NewGuid(), Name = "Project 2" }
        };

        var projectDtos = new List<ProjectDto>
        {
            new ProjectDto { Id = projects[0].Id, Name = "Project 1" },
            new ProjectDto { Id = projects[1].Id, Name = "Project 2" }
        };

        var mockRepository = new Mock<IRepository>();
        mockRepository.Setup(r => r.GetAllAsync(CancellationToken.None)).ReturnsAsync(projects);

        var mockMapper = new Mock<IMapper>();
        mockMapper.Setup(m => m.Map<List<Project>, List<ProjectDto>>(projects)).Returns(projectDtos);

        var handler = new GetProjectsQueryHandler(mockRepository.Object, mockMapper.Object);
        var request = new GetProjectsQuery();

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(projectDtos.Count, result.Count);
        Assert.Equal(projectDtos[0].Id, result[0].Id);
        Assert.Equal(projectDtos[1].Name, result[1].Name);

        mockRepository.Verify(r => r.GetAllAsync(CancellationToken.None), Times.Once);
        mockMapper.Verify(m => m.Map<List<Project>, List<ProjectDto>>(projects), Times.Once);
    }
}