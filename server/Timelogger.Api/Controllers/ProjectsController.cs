using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Timelogger.Application.Projects;
using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Timelogger.Application.Projects.Commands;
using Timelogger.Application.Projects.Queries;

namespace Timelogger.Api.Controllers;

[ApiController]
[Route("api/v1/projects")]
public class ProjectsController : Controller
{
    private IMediator _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    [HttpGet]
    public async Task<ActionResult<List<ProjectDto>>> GetAsync()
    {
        return await Mediator.Send(new GetProjectsQuery());
    }

    [HttpGet("{id}/timelines")]
    public async Task<ActionResult<ProjectDto>> GetTimelineAsync(Guid id)
    {
        return await Mediator.Send(new GetProjectTimelinesQuery{ProjectId = id});
    }

    [HttpPut("{id}/timelines")]
    public async Task<ActionResult<Guid>> PuTimelineAsync(CreateProjectTimelineCommand command)
    {
        return await Mediator.Send(command);
    }
}