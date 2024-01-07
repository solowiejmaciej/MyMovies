#region

using MyMovies.Extensions;
using MyMovies.Middleware;

#endregion

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var corsOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsOrigins,
        builder =>
        {
            builder.WithOrigins(
                    "http://localhost", "https://localhost", "http://localhost:5173", "https://localhost:5173",
                    "http://localhost:3000", "https://localhost:3000", "http://localhost:8081", "https://localhost:8081"
                )
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGeneralServiceCollection(configuration);
builder.Services.AddValidationServiceCollectionExtension();
builder.Services.AddThirdPartyMoviesServiceCollectionExtension(configuration);

builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);


var app = builder.Build();
app.UseCors(corsOrigins);

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ErrorHandlingMiddleware>();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();