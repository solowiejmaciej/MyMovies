#region

using System.Net;
using Microsoft.Extensions.Options;
using MyMovies.Dtos;
using MyMovies.Interfaces;
using MyMovies.Models.Options;
using Newtonsoft.Json;
using RestSharp;

#endregion

namespace MyMovies.Clients;

public class ThirdPartyMovieServiceClient : IThirdPartyMovieServiceClient
{
    private readonly ILogger<ThirdPartyMovieServiceClient> _logger;
    private readonly IOptions<ThirdPartyMoviesClientOptions> _options;

    public ThirdPartyMovieServiceClient(
        ILogger<ThirdPartyMovieServiceClient> logger,
        IOptions<ThirdPartyMoviesClientOptions> options
    )
    {
        _logger = logger;
        _options = options;
    }

    /// <summary>
    ///     Retrieves all movies from the third party service, deserializes them and returns them as a list of MovieDto.
    /// </summary>
    /// <param name="cancellationToken"> CancellationToken token to stop async operation </param>
    /// <returns></returns>
    public async Task<IEnumerable<MovieDto>?> GetMoviesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Getting movies from {_options.Value.BaseUrl}");
        var options = new RestClientOptions(_options.Value.BaseUrl)
        {
            MaxTimeout = -1
        };
        var client = new RestClient(options);
        var request = new RestRequest("/MyMovies");
        var response = await client.ExecuteAsync(request, cancellationToken);
        if (!response.IsSuccessful)
        {
            _logger.LogError(response.ErrorMessage);
            return new List<MovieDto>();
        }

        if (response.StatusCode != HttpStatusCode.OK)
        {
            _logger.LogError($"Error getting movies from {_options.Value.BaseUrl}. Status code: {response.StatusCode}");
            return new List<MovieDto>();
        }

        if (string.IsNullOrEmpty(response.Content)) return new List<MovieDto>();

        var movies = JsonConvert.DeserializeObject<IEnumerable<MovieDto>>(response.Content);
        return movies;
    }
}