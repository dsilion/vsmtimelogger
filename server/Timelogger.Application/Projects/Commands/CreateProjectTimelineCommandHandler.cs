using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Timelogger.Application.Projects.Commands;

public class CreateProjectTimelineCommandHandler : IRequestHandler<CreateProjectTimelineCommand, Guid>
{
    private readonly IRepository _repository;


    public CreateProjectTimelineCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateProjectTimelineCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetByIdAsync(request.ProjectId, cancellationToken);

        project?.AddTimeline(request.StartDate, request.EndDate);

        await _repository.SaveChangesAsync(cancellationToken);

        return project.Id;
    }
}