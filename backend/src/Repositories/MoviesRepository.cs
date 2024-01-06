using Microsoft.EntityFrameworkCore;
using MyMovies.Entities;
using MyMovies.Interfaces;

namespace MyMovies.Repositories;


/// <summary>
/// This class is responsible for handling all the database operations related to movies.
/// It implements the IMoviesRepository interface.
/// </summary>
public class MoviesRepository : IMoviesRepository
{
    private readonly MoviesDbContext _moviesDbContext;

    public MoviesRepository(
        MoviesDbContext moviesDbContext
        )
    {
        _moviesDbContext = moviesDbContext;
    }

    /// <summary>
    ///  Adds a movie to the database.
    /// </summary>
    /// <param name="movie"> Movie entity</param>
    /// <param name="cancellationToken">CancellationToken token to stop async operation</param>
    /// <returns>int(movieId)</returns>
    public async Task<int> AddMovieAsync(Movie? movie, CancellationToken cancellationToken = default)
    {
        var result = await _moviesDbContext.Movies.AddAsync(movie, cancellationToken);
        await _moviesDbContext.SaveChangesAsync(cancellationToken);
        return result.Entity.Id;
    }

    /// <summary>
    /// Deletes a movie from the database.
    /// </summary>
    /// <param name="movie">Movie entity</param>
    /// <param name="cancellationToken">CancellationToken token to stop async operation</param>
    public async Task DeleteMovieByIdAsync(Movie? movie, CancellationToken cancellationToken = default)
    {
        _moviesDbContext.Movies.Remove(movie);
        await _moviesDbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// This method retrieves a movie by id.
    /// </summary>
    /// <param name="id">Id of movie</param>
    /// <param name="cancellationToken">CancellationToken token to stop async operation</param>
    /// <returns>First movie that matches provided id.</returns>
    public async Task<Movie?> GetMovieByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _moviesDbContext.Movies.FirstOrDefaultAsync(movie => movie.Id == id, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Retrieves all movies at once from the database.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the IEnumerable of movies.
    /// </returns>
    public async Task<IEnumerable<Movie?>> GetMoviesAsync(CancellationToken cancellationToken = default)
    {
        return _moviesDbContext.Movies.AsEnumerable();
    }

    /// <summary>
    ///  Updates a movie in the database.
    /// </summary>
    /// <param name="movie"> Movie entity </param>
    /// <param name="cancellationToken"> CancellationToken token to stop async operation </param>
    /// <returns> A task that represents the asynchronous operation. The task result contains the Movie.  </returns>
    public async Task<Movie?> UpdateMovieAsync(Movie? movie, CancellationToken cancellationToken = default)
    {
        var result = _moviesDbContext.Movies.Update(movie);
        await _moviesDbContext.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }
    
}