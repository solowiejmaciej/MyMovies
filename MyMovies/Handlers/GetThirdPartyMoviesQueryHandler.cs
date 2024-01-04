using AutoMapper;
using MediatR;
using MyMovies.Entities;
using MyMovies.Interfaces;

namespace MyMovies.Handlers;

public class GetThirdPartyMoviesQueryHandler : IRequestHandler<GetThirdPartyMoviesQuery, IEnumerable<Movie>>
{
    private readonly IThirdPartyMovieServiceClient _thirdPartyMovieServiceClient;
    private readonly IMovieService _movieService;
    private readonly IMapper _mapper;

    public GetThirdPartyMoviesQueryHandler(
        IThirdPartyMovieServiceClient thirdPartyMovieServiceClient,
        IMovieService movieService,
        IMapper mapper
        )
    {
        _thirdPartyMovieServiceClient = thirdPartyMovieServiceClient;
        _movieService = movieService;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Movie>> Handle(GetThirdPartyMoviesQuery request, CancellationToken cancellationToken)
    {
        var movies = await _thirdPartyMovieServiceClient.GetMoviesAsync();
        var mappedMovies = _mapper.Map<IEnumerable<Movie>>(movies);

        await _movieService.AddMissingMoviesAsync(mappedMovies);
        return mappedMovies;
    }
}

public record GetThirdPartyMoviesQuery : IRequest<IEnumerable<Movie>>;