#region

using AutoMapper;
using Moq;
using MyMovies.Dtos;
using MyMovies.Entities;
using MyMovies.Handlers;
using MyMovies.Interfaces;

#endregion

namespace MyMovies.Tests.Handlers;

public class GetMoviesQueryHandlerTests
{
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IMoviesRepository> _mockMoviesRepository;
    private readonly GetMoviesQueryHandler _handler;

    public GetMoviesQueryHandlerTests()
    {
        _mockMapper = new Mock<IMapper>();
        _mockMoviesRepository = new Mock<IMoviesRepository>();
        _handler = new GetMoviesQueryHandler(_mockMoviesRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnMovies_WhenMoviesExist()
    {
        var query = new GetMoviesQuery();
        var movies = new List<Movie> { new() { Id = 1, Title = "Test Movie", Year = 2022 } };
        var moviesDto = new List<MovieDto> { new() { Id = 1, Title = "Test Movie", Year = 2022 } };

        _mockMoviesRepository.Setup(r => r.GetMoviesAsync(CancellationToken.None)).ReturnsAsync(movies);
        _mockMapper.Setup(m => m.Map<IEnumerable<MovieDto>>(movies)).Returns(moviesDto);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.Equal(moviesDto, result);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmpty_WhenNoMoviesExist()
    {
        var query = new GetMoviesQuery();
        var movies = new List<Movie>();
        var moviesDto = new List<MovieDto>();

        _mockMoviesRepository.Setup(r => r.GetMoviesAsync(CancellationToken.None)).ReturnsAsync(movies);
        _mockMapper.Setup(m => m.Map<IEnumerable<MovieDto>>(movies)).Returns(moviesDto);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.Empty(result);
    }
}