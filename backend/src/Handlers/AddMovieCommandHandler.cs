#region

using AutoMapper;
using FluentValidation;
using MediatR;
using MyMovies.Dtos;
using MyMovies.Entities;
using MyMovies.Interfaces;

#endregion

namespace MyMovies.Handlers;

public class AddMovieCommandHandler : IRequestHandler<AddMovieCommand, MovieDto>
{
    private readonly IMapper _mapper;
    private readonly IMoviesRepository _moviesRepository;

    public AddMovieCommandHandler(
        IMoviesRepository moviesRepository,
        IMapper mapper
    )
    {
        _mapper = mapper;
        _moviesRepository = moviesRepository;
    }

    public async Task<MovieDto> Handle(AddMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = _mapper.Map<Movie>(request);
        await _moviesRepository.AddMovieAsync(movie, cancellationToken);
        return _mapper.Map<MovieDto>(movie);
    }
}

public record AddMovieCommand : IRequest<MovieDto>
{
    public string Title { get; set; }
    public string? Director { get; set; }
    public int Year { get; set; }
    public double? Rate { get; set; }
}

public class AddMovieCommandValidator : AbstractValidator<AddMovieCommand>
{
    public AddMovieCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty()
            .MaximumLength(200);
        RuleFor(x => x.Year).NotEmpty()
            .InclusiveBetween(1900, 2200);
    }
}