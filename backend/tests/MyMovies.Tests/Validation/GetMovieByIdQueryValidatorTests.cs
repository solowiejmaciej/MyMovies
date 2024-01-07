#region

using FluentValidation.TestHelper;
using MyMovies.Handlers;

#endregion

namespace MyMovies.Tests.Validation;

public class GetMovieByIdQueryValidatorTests
{
    private readonly GetMovieByIdQueryValidator _validator;

    public GetMovieByIdQueryValidatorTests()
    {
        _validator = new GetMovieByIdQueryValidator();
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenMovieIdIsLessThanOne()
    {
        var query = new GetMovieByIdQuery(0);
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.MovieId);
    }

    [Fact]
    public void Validate_ShouldNotHaveError_WhenCommandIsValid()
    {
        var query = new GetMovieByIdQuery(1);
        var result = _validator.TestValidate(query);
        result.ShouldNotHaveAnyValidationErrors();
    }
}