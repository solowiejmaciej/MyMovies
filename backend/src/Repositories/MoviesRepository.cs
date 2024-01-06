using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyMovies.Entities;
using MyMovies.Exceptions;
using MyMovies.Interfaces;

namespace MyMovies.Repositories;

public class MoviesRepository : IMoviesRepository
{
    private readonly MoviesDbContext _moviesDbContext;

    public MoviesRepository(
        MoviesDbContext moviesDbContext
        )
    {
        _moviesDbContext = moviesDbContext;
    }
    public async Task<int> AddMovieAsync(Movie? movie)
    {
        var result = await _moviesDbContext.Movies.AddAsync(movie);
        await _moviesDbContext.SaveChangesAsync();
        return result.Entity.Id;
    }

    public async Task DeleteMovieByIdAsync(Movie? movie)
    {
        _moviesDbContext.Movies.Remove(movie);
        await _moviesDbContext.SaveChangesAsync();
    }

    public async Task<Movie?> GetMovieByIdAsync(int id)
    {
        return await _moviesDbContext.Movies.FirstOrDefaultAsync(movie => movie.Id == id);
    }

    public async Task<IEnumerable<Movie?>> GetMoviesAsync()
    {
        return _moviesDbContext.Movies.AsEnumerable();
    }

    public async Task<Movie?> UpdateMovieAsync(Movie? movie)
    {
        var result = _moviesDbContext.Movies.Update(movie);
        await _moviesDbContext.SaveChangesAsync();
        return result.Entity;
    }
    
}