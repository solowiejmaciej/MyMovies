using MyMovies.Handlers;

namespace MyMovies.Tests.Validation;

using Xunit;
using FluentValidation.TestHelper;
using Handlers;

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