using Xunit;
using Moq;
using System.Threading;
using AutoMapper;
using MyMovies.Handlers;
using MyMovies.Interfaces;
using MyMovies.Entities;
using System.Collections.Generic;
using MyMovies.Dtos;
using MyMovies.MappingProfiles;

public class GetThirdPartyMoviesCommandHandlerTests
{
    private readonly Mock<IThirdPartyMovieServiceClient> _mockThirdPartyMovieServiceClient;
    private readonly Mock<IMovieService> _mockMovieService;
    private readonly GetThirdPartyMoviesCommandHandler _handler;

    public GetThirdPartyMoviesCommandHandlerTests()
    {
        _mockThirdPartyMovieServiceClient = new Mock<IThirdPartyMovieServiceClient>();
        _mockMovieService = new Mock<IMovieService>();
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MovieMappingProfile>());
        var mapper = config.CreateMapper();
        _handler = new GetThirdPartyMoviesCommandHandler(_mockThirdPartyMovieServiceClient.Object, _mockMovieService.Object, mapper);
    }

    [Fact]
    public async Task Handle_ShouldReturnMovies_WhenMoviesExist()
    {
        var query = new GetThirdPartyMoviesCommand();
        var moviesDtos = new List<MovieDto> { new MovieDto { Id = 1, Title = "Test Movie", Year = 2022 } };

        _mockThirdPartyMovieServiceClient.Setup(c => c.GetMoviesAsync(CancellationToken.None)).ReturnsAsync(moviesDtos);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmpty_WhenNoMoviesExist()
    {
        var query = new GetThirdPartyMoviesCommand();
        var movies = new List<Movie>();
        var moviesDtos = new List<MovieDto>();

        _mockThirdPartyMovieServiceClient.Setup(c => c.GetMoviesAsync(CancellationToken.None)).ReturnsAsync(moviesDtos);

        var result = await _handler.Handle(query, CancellationToken.None);

        _mockMovieService.Verify(s => s.AddMissingMoviesAsync(movies,CancellationToken.None), Times.Once);
        Assert.Empty(result);
    }
}