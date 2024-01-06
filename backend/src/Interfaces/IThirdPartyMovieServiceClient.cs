using MyMovies.Dtos;
using MyMovies.Entities;

namespace MyMovies.Interfaces;


/// <summary>
/// This class is responsible for handling all third party movie service operations.
/// </summary>
public interface IThirdPartyMovieServiceClient
{
    /// <summary>
    ///  Retrieves all movies from the third party service, deserializes them and returns them as a list of MovieDto.
    /// </summary>
    /// <param name="cancellationToken"> CancellationToken token to stop async operation </param>
    /// <returns>  A task that represents the asynchronous operation. The task result contains the IEnumerable of moviesDtos.  </returns>
    public Task<IEnumerable<MovieDto>> GetMoviesAsync(CancellationToken cancellationToken = default);
}