#region

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MyMovies.Entities;
using MyMovies.Interfaces;
using MyMovies.Middleware;
using MyMovies.Repositories;

#endregion

namespace MyMovies.Extensions;

public static class GeneralServiceCollectionExtension
{
    public static void AddGeneralServiceCollection(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
            connectionString = configuration.GetConnectionString("Docker");

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddDbContext<MoviesDbContext>(options => { options.UseSqlServer(connectionString); });

        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<MoviesDbContext>();
            
            context.Database.Migrate();
        }
        
        services.AddScoped<IMoviesRepository, MoviesRepository>();
        services.AddScoped<ErrorHandlingMiddleware>();

    }
}