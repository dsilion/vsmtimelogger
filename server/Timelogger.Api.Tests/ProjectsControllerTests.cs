using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Timelogger.Api.Controllers;
using Timelogger.Application.Projects.Commands;
using Timelogger.Application.Projects;
using Timelogger.Application.Projects.Queries;
using Xunit;

namespace Timelogger.Api.Tests;

public class ProjectsControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly ProjectsController _controller;

    public ProjectsControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new ProjectsController { ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() } };
        _controller.HttpContext.RequestServices = Mock.Of<IServiceProvider>(_ => _.GetService(typeof(IMediator)) == _mediatorMock.Object);
    }

    [Fact]
    public async Task GetAsync_ReturnsListOfProjectDtos()
    {
        // Arrange
        var expectedProjects = new List<ProjectDto>
        {
            new() { Id = Guid.NewGuid(),  Name = "Project 1" },
            new() {Id = Guid.NewGuid(), Name = "Project 2"}
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetProjectsQuery>(), default)).ReturnsAsync(expectedProjects);

        // Act
        var result = await _controller.GetAsync();

        // Assert
        var actionResult = Assert.IsType<ActionResult<List<ProjectDto>>>(result);
        var projectDtos = Assert.IsAssignableFrom<List<ProjectDto>>(actionResult.Value);
        Assert.Equal(expectedProjects.Count, projectDtos.Count);
    }

    [Fact]
    public async Task GetTimelineAsync_ValidId_ReturnsProjectDtoWithTimelines()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var expectedProjectDto = new ProjectDto { Id = projectId, Name = "Project 1" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetProjectTimelinesQuery>(), default)).ReturnsAsync(expectedProjectDto);

        // Act
        var result = await _controller.GetTimelineAsync(projectId);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ProjectDto>>(result);
        var projectDto = Assert.IsAssignableFrom<ProjectDto>(actionResult.Value);
        Assert.Equal(projectId, projectDto.Id);
    }

    [Fact]
    public async Task PuTimelineAsync_ValidCommand_ReturnsProjectId()
    {
        // Arrange
        var command = new CreateProjectTimelineCommand { ProjectId = Guid.NewGuid(), StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(7) };
        var expectedProjectId = command.ProjectId;
        _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(expectedProjectId);

        // Act
        var result = await _controller.PuTimelineAsync(command);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Guid>>(result);
        Assert.Equal(expectedProjectId, actionResult.Value);
    }
}