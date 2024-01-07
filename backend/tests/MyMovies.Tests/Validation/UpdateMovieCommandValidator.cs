#region

using FluentValidation.TestHelper;
using MyMovies.Handlers;

#endregion

namespace MyMovies.Tests.Validation;

public class UpdateMovieCommandValidatorTests
{
    private readonly UpdateMovieCommandValidator _validator;

    public UpdateMovieCommandValidatorTests()
    {
        _validator = new UpdateMovieCommandValidator();
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenTitleIsEmpty()
    {
        var command = new UpdateMovieCommand { Title = "", Year = 2022 };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenTitleIsTooLong()
    {
        var command = new UpdateMovieCommand { Title = new string('a', 201), Year = 2022 };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenYearIsOutOfRange()
    {
        var command = new UpdateMovieCommand { Title = "Test Movie", Year = 1899 };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Year);

        command.Year = 2201;
        result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Year);
    }

    [Fact]
    public void Validate_ShouldNotHaveError_WhenCommandIsValid()
    {
        var command = new UpdateMovieCommand { Title = "Test Movie", Year = 2022 };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}