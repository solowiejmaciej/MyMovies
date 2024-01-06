using Xunit;
using Moq;
using MyMovies.Services;
using MyMovies.Interfaces;
using MyMovies.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

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
            new Movie { Id = 1, Title = "Test Movie", Director = "Test Director", Year = 2022 },
            new Movie { Id = 2, Title = "Test Movie 2", Director = "Test Director 2", Year = 2023 }
        };

        _mockMoviesRepository.Setup(r => r.GetMoviesAsync()).ReturnsAsync(new List<Movie>());

        await _service.AddMissingMoviesAsync(movies);

        _mockMoviesRepository.Verify(r => r.AddMovieAsync(It.IsAny<Movie>()), Times.Exactly(movies.Count));
    }

    [Fact]
    public async Task AddMissingMoviesAsync_ShouldNotAddExistingMovies()
    {
        var movies = new List<Movie> 
        { 
            new Movie { Id = 1, Title = "Test Movie", Director = "Test Director", Year = 2022 },
            new Movie { Id = 2, Title = "Test Movie 2", Director = "Test Director 2", Year = 2023 }
        };

        _mockMoviesRepository.Setup(r => r.GetMoviesAsync()).ReturnsAsync(movies);

        await _service.AddMissingMoviesAsync(movies);

        _mockMoviesRepository.Verify(r => r.AddMovieAsync(It.IsAny<Movie>()), Times.Never);
    }
}