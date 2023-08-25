using FluentValidation;

namespace Timelogger.Application.Projects.Commands;

public class CreateProjectTimelineCommandValidator : AbstractValidator<CreateProjectTimelineCommand>
{
    public CreateProjectTimelineCommandValidator()
    {
        RuleFor(v => v.EndDate)
            .GreaterThan(r => r.StartDate)
            .WithMessage("End Date should be greater than start date.");
    }
}