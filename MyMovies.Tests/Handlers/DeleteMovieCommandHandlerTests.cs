using Xunit;
using Moq;
using System.Threading;
using MyMovies.Entities;
using MyMovies.Exceptions;
using MyMovies.Handlers;
using MyMovies.Interfaces;

public class DeleteMovieCommandHandlerTests
{
    private readonly Mock<IMoviesRepository> _mockMoviesRepository;
    private readonly DeleteMovieCommandHandler _handler;

    public DeleteMovieCommandHandlerTests()
    {
        _mockMoviesRepository = new Mock<IMoviesRepository>();
        _handler = new DeleteMovieCommandHandler(_mockMoviesRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldDeleteMovie_WhenCommandIsValid()
    {
        var command = new DeleteMovieCommand(1);
        var movie = new Movie { Id = 1, Title = "Test Movie", Year = 2022 };

        _mockMoviesRepository.Setup(r => r.GetMovieByIdAsync(command.MovieId)).ReturnsAsync(movie);

        await _handler.Handle(command, CancellationToken.None);

        _mockMoviesRepository.Verify(r => r.DeleteMovieByIdAsync(movie), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenMovieDoesNotExist()
    {
        var command = new DeleteMovieCommand(1);

        _mockMoviesRepository.Setup(r => r.GetMovieByIdAsync(command.MovieId)).ReturnsAsync((Movie)null);

        await Assert.ThrowsAsync<MovieNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
