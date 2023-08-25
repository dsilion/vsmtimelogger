using Timelogger.Application.Projects.Commands;
using FluentValidation.TestHelper;

namespace Timelogger.Application.Tests.Commands;

public class CreateProjectTimelineCommandValidatorTests
{
    private readonly CreateProjectTimelineCommandValidator _validator = new();

    [Fact]
    public void EndDate_GratherThanStartDateShouldNotHaveValidationError()
    {
        // Arrange
        var command = new CreateProjectTimelineCommand
        {
            StartDate = DateTime.Parse("2023-08-24T12:00:00"),
            EndDate = DateTime.Parse("2023-08-24T12:30:00")
        };

        // Act & Assert
        _validator.TestValidate(command).ShouldNotHaveValidationErrorFor(c => c.EndDate);
    }

    [Fact]
    public void EndDate_LessThanStartDateShouldHaveValidationErrorWithErrorMessage()
    {
        // Arrange
        var command = new CreateProjectTimelineCommand
        {
            StartDate = DateTime.Parse("2023-08-24T11:45:00"),
            EndDate = DateTime.Parse("2023-08-24T11:30:00")
        };

        // Act & Assert
        _validator.TestValidate(command).ShouldHaveValidationErrorFor(c => c.EndDate)
            .WithErrorMessage("End Date should be greater than start date.");
    }
}