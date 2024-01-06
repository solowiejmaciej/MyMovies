using MyMovies.Entities;
using MyMovies.Interfaces;

namespace MyMovies.Services;

public class MovieService : IMovieService
{
    private readonly IMoviesRepository _moviesRepository;
    private readonly ILogger<MovieService> _logger;

    public MovieService(
        IMoviesRepository moviesRepository,
        ILogger<MovieService> logger
        )
    {
        _moviesRepository = moviesRepository;
        _logger = logger;
    }
    
    /// <summary>
    /// Adds missing movies to the database from the list of movies provided
    /// </summary>
    /// <param name="movies"> List of potential missing movies from db</param>
    /// <param name="cancellationToken"> CancellationToken token to stop async operation </param>
    public async Task AddMissingMoviesAsync(IEnumerable<Movie> movies , CancellationToken cancellationToken)
    {
        var existingMovies = await _moviesRepository.GetMoviesAsync(cancellationToken);
        var missingMovies = movies.Where(movie => !existingMovies.Any(existingMovie => existingMovie.Title == movie.Title && existingMovie.Director == movie.Director && existingMovie.Year == movie.Year));
        var enumerable = missingMovies.ToList();
        _logger.LogInformation($"Found {enumerable.Count()} missing movies");
        foreach (var missingMovie in enumerable)
        {
            var movie = new Movie()
            {
                Title = missingMovie.Title,
                Director = missingMovie.Director,
                Year = missingMovie.Year,
                Rate = missingMovie.Rate,
                ThirdPartyId = missingMovie.Id
            };
            await _moviesRepository.AddMovieAsync(movie, cancellationToken);
        }
    }
}