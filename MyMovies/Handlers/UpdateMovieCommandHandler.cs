using AutoMapper;
using FluentValidation;
using MediatR;
using MyMovies.Dtos;
using MyMovies.Entities;
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
        var movie = await _moviesRepository.GetMovieByIdAsync(request.Id);
        movie.Title = request.Title;
        movie.Director = request.Director;
        movie.Year = request.Year;
        movie.Rate = request.Rate;
        var updatedMovie = await _moviesRepository.UpdateMovieAsync(movie);
        return _mapper.Map<MovieDto>(updatedMovie);
    }
}

public record UpdateMovieCommand : IRequest<MovieDto>
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Director { get; set; }
    public required int Year { get; set; }
    public required double Rate { get; set; }
}

public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
{
    public UpdateMovieCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Director).NotEmpty();
        RuleFor(x => x.Year).NotEmpty();
        RuleFor(x => x.Rate).NotEmpty();
    }
}