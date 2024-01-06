using MyMovies.Entities;

namespace MyMovies.Interfaces;

public interface IMovieService
{
    /// <summary>
    /// Adds missing movies to the database from the list of movies provided
    /// </summary>
    /// <param name="movies"> List of potential missing movies from db</param>
    /// <param name="cancellationToken"> CancellationToken token to stop async operation </param>
    public Task AddMissingMoviesAsync(IEnumerable<Movie> movies, CancellationToken cancellationToken = default);
}