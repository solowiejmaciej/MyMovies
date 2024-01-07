#region

using AutoMapper;
using MediatR;
using MyMovies.Dtos;
using MyMovies.Interfaces;

#endregion

namespace MyMovies.Handlers;

public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, IEnumerable<MovieDto>>
{
    private readonly IMoviesRepository _movieRepository;
    private readonly IMapper _mapper;

    public GetMoviesQueryHandler(
        IMoviesRepository movieRepository,
        IMapper mapper
    )
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MovieDto>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
    {
        var movies = await _movieRepository.GetMoviesAsync(cancellationToken);
        var moviesDtos = _mapper.Map<IEnumerable<MovieDto>>(movies);
        return moviesDtos;
    }
}

public record GetMoviesQuery : IRequest<IEnumerable<MovieDto>>
{
}