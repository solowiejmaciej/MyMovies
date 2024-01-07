#region

using Microsoft.EntityFrameworkCore;

#endregion

namespace MyMovies.Entities;

public sealed class MoviesDbContext : DbContext
{
    public MoviesDbContext(DbContextOptions<MoviesDbContext> options) : base(options)
    {
    }

    public DbSet<Movie?> Movies { get; set; }
}