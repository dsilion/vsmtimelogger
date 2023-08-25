using MediatR;
using System.Collections.Generic;

namespace Timelogger.Application.Projects.Queries;

public class GetProjectsQuery : IRequest<List<ProjectDto>>
{
}