using AutoMapper;
using FluentValidation;
using MediatR;
using MyMovies.Dtos;
using MyMovies.Entities;
using MyMovies.Exceptions;
using MyMovies.Interfaces;

namespace MyMovies.Handlers;

public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, MovieDto>
{
    private readonly IMoviesRepository _movieRepository;
    private readonly IMapper _mapper;

    public GetMovieByIdQueryHandler(IMoviesRepository movieRepository, IMapper mapper)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
    }

    public async Task<MovieDto> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetMovieByIdAsync(request.MovieId, cancellationToken);
        if (movie is null)
        {
            throw new MovieNotFoundException(request.MovieId);
        }
        return _mapper.Map<MovieDto>(movie);
    }
}

public record GetMovieByIdQuery(int MovieId) : IRequest<MovieDto>
{
    public int MovieId { get; set; } = MovieId;
}

public class GetMovieByIdQueryValidator : AbstractValidator<GetMovieByIdQuery>
{
    public GetMovieByIdQueryValidator()
    {
        RuleFor(query => query.MovieId).GreaterThan(0).NotEmpty();
    }
}