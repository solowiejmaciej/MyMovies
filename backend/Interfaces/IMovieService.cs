using MyMovies.Dtos;
using MyMovies.Entities;

namespace MyMovies.Interfaces;

public interface IMovieService
{
    public Task AddMissingMoviesAsync(IEnumerable<Movie> movies);
}