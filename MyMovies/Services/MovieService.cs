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
    public async Task AddMissingMoviesAsync(IEnumerable<Movie> movies)
    {
        var existingMovies = await _moviesRepository.GetMoviesAsync();
        var missingMovies = movies.Where(movie => !existingMovies.Any(existingMovie => existingMovie.Title == movie.Title && existingMovie.Director == movie.Director && existingMovie.Year == movie.Year));
        _logger.LogInformation($"Found {missingMovies.Count()} missing movies");
        foreach (var missingMovie in missingMovies)
        {
            var movie = new Movie()
            {
                Title = missingMovie.Title,
                Director = missingMovie.Director,
                Year = missingMovie.Year,
                Rate = missingMovie.Rate,
                ThirdPartyId = missingMovie.Id
            };
            await _moviesRepository.AddMovieAsync(movie);
        }
    }
}