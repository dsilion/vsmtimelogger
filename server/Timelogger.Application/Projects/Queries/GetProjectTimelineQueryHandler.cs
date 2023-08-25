using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Timelogger.Entities;

namespace Timelogger.Application.Projects.Queries;

public class GetProjectTimelinesQueryHandler : IRequestHandler<GetProjectTimelinesQuery, ProjectDto>
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public GetProjectTimelinesQueryHandler(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProjectDto> Handle(GetProjectTimelinesQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetByIdWithTimelineAsync(request.ProjectId, cancellationToken);
        return _mapper.Map<Project, ProjectDto>(result);
    }
}