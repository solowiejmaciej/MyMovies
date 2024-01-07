#region

using FluentValidation;
using FluentValidation.AspNetCore;
using MyMovies.Handlers;

#endregion

namespace MyMovies.Extensions;

public static class ValidationServiceCollectionExtension
{
    public static void AddValidationServiceCollectionExtension(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();

        services.AddScoped<IValidator<AddMovieCommand>, AddMovieCommandValidator>();
        services.AddScoped<IValidator<UpdateMovieCommand>, UpdateMovieCommandValidator>();
        services.AddScoped<IValidator<DeleteMovieCommand>, DeleteMovieCommandValidator>();
        services.AddScoped<IValidator<GetMovieByIdQuery>, GetMovieByIdQueryValidator>();
    }
}