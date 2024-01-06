using System.Linq.Expressions;
using MyMovies.Entities;

namespace MyMovies.Interfaces;

public interface IMoviesRepository
{
    /// <summary>
    ///  Adds a movie to the database.
    /// </summary>
    /// <param name="movie"> Movie entity</param>
    /// <param name="cancellationToken">CancellationToken token to stop async operation</param>
    /// <returns>int(movieId)</returns>
    Task<int> AddMovieAsync(Movie? movie, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Deletes a movie from the database.
    /// </summary>
    /// <param name="movie">Movie entity</param>
    /// <param name="cancellationToken">CancellationToken token to stop async operation</param>
    Task DeleteMovieByIdAsync(Movie? movie, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// This method retrieves a movie by id.
    /// </summary>
    /// <param name="id">Id of movie</param>
    /// <param name="cancellationToken">CancellationToken token to stop async operation</param>
    /// <returns>First movie that matches provided id.</returns>
    Task<Movie?> GetMovieByIdAsync(int id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Retrieves all movies at once from the database.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the IEnumerable of movies.
    /// </returns>
    Task<IEnumerable<Movie?>> GetMoviesAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    ///  Updates a movie in the database.
    /// </summary>
    /// <param name="movie"> Movie entity </param>
    /// <param name="cancellationToken"> CancellationToken token to stop async operation </param>
    /// <returns> A task that represents the asynchronous operation. The task result contains the Movie.  </returns>
    Task<Movie?> UpdateMovieAsync(Movie? movie, CancellationToken cancellationToken = default);
}