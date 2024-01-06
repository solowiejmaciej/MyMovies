using Xunit;
using FluentValidation.TestHelper;
using MyMovies.Handlers;

public class AddMovieCommandValidatorTests
{
    private readonly AddMovieCommandValidator _validator;

    public AddMovieCommandValidatorTests()
    {
        _validator = new AddMovieCommandValidator();
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenTitleIsEmpty()
    {
        var command = new AddMovieCommand { Title = "", Year = 2022 };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenYearIsOutOfRange()
    {
        var command = new AddMovieCommand { Title = "Test Movie", Year = 1899 };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Year);

        command.Year = 2201;
        result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Year);
    }

    [Fact]
    public void Validate_ShouldNotHaveError_WhenCommandIsValid()
    {
        var command = new AddMovieCommand { Title = "Test Movie", Year = 2022 };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}