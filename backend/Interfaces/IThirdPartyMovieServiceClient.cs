using MyMovies.Dtos;
using MyMovies.Entities;

namespace MyMovies.Interfaces;

public interface IThirdPartyMovieServiceClient
{
    public Task<IEnumerable<MovieDto>> GetMoviesAsync();
}