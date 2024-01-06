using Xunit;
using Moq;
using System.Threading;
using AutoMapper;
using MyMovies.Handlers;
using MyMovies.Interfaces;
using MyMovies.Entities;
using MyMovies.Dtos;
using MyMovies.Exceptions;

public class UpdateMovieCommandHandlerTests
{
    private readonly Mock<IMoviesRepository> _mockMoviesRepository;
    private readonly IMapper _mapper;
    private readonly UpdateMovieCommandHandler _handler;

    public UpdateMovieCommandHandlerTests()
    {
        _mockMoviesRepository = new Mock<IMoviesRepository>();
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Movie, MovieDto>());
        _mapper = config.CreateMapper();
        _handler = new UpdateMovieCommandHandler(_mockMoviesRepository.Object, _mapper);
    }

    [Fact]
    public async Task Handle_ShouldUpdateMovie_WhenCommandIsValid()
    {
        var command = new UpdateMovieCommand { Id = 1, Title = "Updated Movie", Year = 2022 };
        var movie = new Movie { Id = 1, Title = "Test Movie", Year = 2022 };
        var updatedMovie = new Movie { Id = 1, Title = "Updated Movie", Year = 2022 };
        var updatedMovieDto = new MovieDto { Id = 1, Title = "Updated Movie", Year = 2022 };

        _mockMoviesRepository.Setup(r => r.GetMovieByIdAsync(command.Id,CancellationToken.None)).ReturnsAsync(movie);
        _mockMoviesRepository.Setup(r => r.UpdateMovieAsync(It.Is<Movie>(m => m.Title == command.Title && m.Year == command.Year),CancellationToken.None)).ReturnsAsync(updatedMovie);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(updatedMovieDto.Title, result.Title);
        Assert.Equal(updatedMovieDto.Year, result.Year);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenMovieDoesNotExist()
    {
        var command = new UpdateMovieCommand { Id = 1, Title = "Updated Movie", Year = 2022 };

        _mockMoviesRepository.Setup(r => r.GetMovieByIdAsync(command.Id,CancellationToken.None)).ReturnsAsync((Movie)null);

        await Assert.ThrowsAsync<MovieNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
