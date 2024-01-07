#region

using FluentValidation.TestHelper;
using MyMovies.Handlers;

#endregion

namespace MyMovies.Tests.Validation;

public class DeleteMovieCommandValidatorTests
{
    private readonly DeleteMovieCommandValidator _validator;

    public DeleteMovieCommandValidatorTests()
    {
        _validator = new DeleteMovieCommandValidator();
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenMovieIdIsLessThanOne()
    {
        var command = new DeleteMovieCommand(0);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.MovieId);
    }

    [Fact]
    public void Validate_ShouldNotHaveError_WhenCommandIsValid()
    {
        var command = new DeleteMovieCommand(1);
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}