#region

using AutoMapper;
using MediatR;
using MyMovies.Entities;
using MyMovies.Interfaces;

#endregion

namespace MyMovies.Handlers;

public class GetThirdPartyMoviesCommandHandler : IRequestHandler<GetThirdPartyMoviesCommand, IEnumerable<Movie>>
{
    private readonly IThirdPartyMovieServiceClient _thirdPartyMovieServiceClient;
    private readonly IMovieService _movieService;
    private readonly IMapper _mapper;

    public GetThirdPartyMoviesCommandHandler(
        IThirdPartyMovieServiceClient thirdPartyMovieServiceClient,
        IMovieService movieService,
        IMapper mapper
    )
    {
        _thirdPartyMovieServiceClient = thirdPartyMovieServiceClient;
        _movieService = movieService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Movie>> Handle(GetThirdPartyMoviesCommand request,
        CancellationToken cancellationToken)
    {
        var movies = await _thirdPartyMovieServiceClient.GetMoviesAsync(cancellationToken);
        var mappedMovies = _mapper.Map<IEnumerable<Movie>>(movies);

        await _movieService.AddMissingMoviesAsync(mappedMovies, cancellationToken);
        return mappedMovies;
    }
}

public record GetThirdPartyMoviesCommand : IRequest<IEnumerable<Movie>>;