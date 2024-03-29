#region

using AutoMapper;
using Moq;
using MyMovies.Dtos;
using MyMovies.Entities;
using MyMovies.Handlers;
using MyMovies.Interfaces;

#endregion

namespace MyMovies.Tests.Handlers;

public class AddMovieCommandHandlerTests
{
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IMoviesRepository> _mockMoviesRepository;
    private readonly AddMovieCommandHandler _handler;

    public AddMovieCommandHandlerTests()
    {
        _mockMapper = new Mock<IMapper>();
        _mockMoviesRepository = new Mock<IMoviesRepository>();
        _handler = new AddMovieCommandHandler(_mockMoviesRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ShouldAddMovie_WhenCommandIsValid()
    {
        // Arrange
        var command = new AddMovieCommand { Title = "Test Movie", Year = 2022 };
        var movie = new Movie { Title = "Test Movie", Year = 2022 };
        var movieDto = new MovieDto { Title = "Test Movie", Year = 2022 };

        _mockMapper.Setup(m => m.Map<Movie>(command)).Returns(movie);
        _mockMapper.Setup(m => m.Map<MovieDto>(movie)).Returns(movieDto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockMoviesRepository.Verify(r => r.AddMovieAsync(movie, CancellationToken.None), Times.Once);
        Assert.Equal(movieDto, result);
    }
}