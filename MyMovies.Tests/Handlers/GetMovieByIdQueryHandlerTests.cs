using AutoMapper;
using Moq;
using MyMovies.Dtos;
using MyMovies.Entities;
using MyMovies.Exceptions;
using MyMovies.Handlers;
using MyMovies.Interfaces;

namespace MyMovies.Tests.Handlers;

public class GetMovieByIdQueryHandlerTests
{
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IMoviesRepository> _mockMoviesRepository;
    private readonly GetMovieByIdQueryHandler _handler;

    public GetMovieByIdQueryHandlerTests()
    {
        _mockMapper = new Mock<IMapper>();
        _mockMoviesRepository = new Mock<IMoviesRepository>();
        _handler = new GetMovieByIdQueryHandler(_mockMoviesRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnMovie_WhenMovieExists()
    {
        var query = new GetMovieByIdQuery(1);
        var movie = new Movie { Id = 1, Title = "Test Movie", Year = 2022 };
        var movieDto = new MovieDto { Id = 1, Title = "Test Movie", Year = 2022 };

        _mockMoviesRepository.Setup(r => r.GetMovieByIdAsync(query.MovieId)).ReturnsAsync(movie);
        _mockMapper.Setup(m => m.Map<MovieDto>(movie)).Returns(movieDto);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.Equal(movieDto, result);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenMovieDoesNotExist()
    {
        var query = new GetMovieByIdQuery(1);

        _mockMoviesRepository.Setup(r => r.GetMovieByIdAsync(query.MovieId)).ReturnsAsync((Movie)null);

        await Assert.ThrowsAsync<MovieNotFoundException>(() => _handler.Handle(query, CancellationToken.None));
    }
}