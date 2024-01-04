using MyMovies.Clients;
using MyMovies.Interfaces;
using MyMovies.Models.Options;
using MyMovies.Services;

namespace MyMovies.Extensions;

public static class ThirdPartyMoviesServiceCollectionExtension
{
    public static void AddThirdPartyMoviesServiceCollectionExtension(this IServiceCollection services, IConfiguration configuration)
    {
        var thirdPartyMoviesSection = configuration.GetSection("ThirdPartyMoviesClientOptions");
        services.Configure<ThirdPartyMoviesClientOptions>(thirdPartyMoviesSection);        
        services.AddScoped<IThirdPartyMovieServiceClient, ThirdPartyMovieServiceClient>();
        services.AddScoped<IMovieService, MovieService>();
    }
}