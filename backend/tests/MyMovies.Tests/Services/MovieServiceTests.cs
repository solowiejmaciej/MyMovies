#region

using Microsoft.Extensions.Logging;
using Moq;
using MyMovies.Entities;
using MyMovies.Interfaces;
using MyMovies.Services;

#endregion

namespace MyMovies.Tests.Services;

public class MovieServiceTests
{
    private readonly Mock<IMoviesRepository> _mockMoviesRepository;
    private readonly Mock<ILogger<MovieService>> _mockLogger;
    private readonly MovieService _service;

    public MovieServiceTests()
    {
        _mockMoviesRepository = new Mock<IMoviesRepository>();
        _mockLogger = new Mock<ILogger<MovieService>>();
        _service = new MovieService(_mockMoviesRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task AddMissingMoviesAsync_ShouldAddMissingMovies()
    {
        var movies = new List<Movie>
        {
            new() { Id = 1, Title = "Test Movie", Director = "Test Director", Year = 2022 },
            new() { Id = 2, Title = "Test Movie 2", Director = "Test Director 2", Year = 2023 }
        };

        _mockMoviesRepository.Setup(r => r.GetMoviesAsync(CancellationToken.None)).ReturnsAsync(new List<Movie>());

        await _service.AddMissingMoviesAsync(movies, CancellationToken.None);

        _mockMoviesRepository.Verify(r => r.AddMovieAsync(It.IsAny<Movie>(), CancellationToken.None),
            Times.Exactly(movies.Count));
    }

    [Fact]
    public async Task AddMissingMoviesAsync_ShouldNotAddExistingMovies()
    {
        var movies = new List<Movie>
        {
            new() { Id = 1, Title = "Test Movie", Director = "Test Director", Year = 2022 },
            new() { Id = 2, Title = "Test Movie 2", Director = "Test Director 2", Year = 2023 }
        };

        _mockMoviesRepository.Setup(r => r.GetMoviesAsync(CancellationToken.None)).ReturnsAsync(movies);

        await _service.AddMissingMoviesAsync(movies, CancellationToken.None);

        _mockMoviesRepository.Verify(r => r.AddMovieAsync(It.IsAny<Movie>(), CancellationToken.None), Times.Never);
    }
}