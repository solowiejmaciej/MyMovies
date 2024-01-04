using MyMovies.Entities;

namespace MyMovies.Interfaces;

public interface IMoviesRepository
{
    Task<int> AddMovieAsync(Movie movie);
    Task DeleteMovieByIdAsync(Movie movie);
    Task<Movie> GetMovieByIdAsync(int id);
    Task<IEnumerable<Movie>> GetMoviesAsync();
    Task<Movie> UpdateMovieAsync(Movie movie);
}