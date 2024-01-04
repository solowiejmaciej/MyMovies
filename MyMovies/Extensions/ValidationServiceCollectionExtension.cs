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
    }
}