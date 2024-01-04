#region

#endregion

#region

using FluentValidation;
using MyMovies.Exceptions;

#endregion

namespace MyMovies.Middleware;

public class ErrorHandlingMiddleware : IMiddleware

{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (MovieNotFoundException notFoundException)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(notFoundException.Message);
        }        
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            context.Response.StatusCode = 500; 
        }
    }
}