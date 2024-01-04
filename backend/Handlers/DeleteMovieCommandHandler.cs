using FluentValidation;
using MediatR;
using MyMovies.Entities;
using MyMovies.Interfaces;

namespace MyMovies.Handlers;

public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand>
{
    private readonly IMoviesRepository _movieRepository;

    public DeleteMovieCommandHandler(IMoviesRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetMovieByIdAsync(request.MovieId);
        await _movieRepository.DeleteMovieByIdAsync(movie);
    }
}


public record DeleteMovieCommand(int MovieId) : IRequest
{
    public int MovieId { get; set; } = MovieId;
}

public class DeleteMovieCommandValidator : AbstractValidator<DeleteMovieCommand>
{
    public DeleteMovieCommandValidator()
    {
        RuleFor(command => command.MovieId).GreaterThan(0).NotEmpty();
    }
}