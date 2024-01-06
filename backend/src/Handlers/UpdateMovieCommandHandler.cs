using AutoMapper;
using FluentValidation;
using MediatR;
using MyMovies.Dtos;
using MyMovies.Entities;
using MyMovies.Exceptions;
using MyMovies.Interfaces;
using MyMovies.Models.RequestsBody;

namespace MyMovies.Handlers;

public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, MovieDto>
{
    private readonly IMoviesRepository _moviesRepository;
    private readonly IMapper _mapper;

    public UpdateMovieCommandHandler(
        IMoviesRepository moviesRepository,
        IMapper mapper
        )
    {
        _moviesRepository = moviesRepository;
        _mapper = mapper;
    }
    public async Task<MovieDto> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _moviesRepository.GetMovieByIdAsync(request.Id, cancellationToken);
        if (movie is null)
        {
            throw new MovieNotFoundException(request.Id);
        }
        movie.Title = request.Title;
        movie.Director = request.Director;
        movie.Year = request.Year;
        movie.Rate = request.Rate;
        var updatedMovie = await _moviesRepository.UpdateMovieAsync(movie, cancellationToken);
        return _mapper.Map<MovieDto>(updatedMovie);
    }
}

public record UpdateMovieCommand : IRequest<MovieDto>
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Director { get; set; }
    public required int Year { get; set; }
    public double? Rate { get; set; }
}

public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
{
    public UpdateMovieCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty()
            .MaximumLength(200);
        RuleFor(x => x.Year).NotEmpty()
            .InclusiveBetween(1900, 2200);
    }
}