#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyMovies.Handlers;

#endregion

namespace MyMovies.Controllers;

[ApiController]
[Route("api/third-party-movies")]
public class ThirdPartyMoviesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ThirdPartyMoviesController(
        IMediator mediator
    )
    {
        _mediator = mediator;
    }


    /// <summary>
    ///     Retrieves all movies from the third-party service. If the service is unavailable, returns an empty list.
    ///     If missing movie is found, it is added to the database.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the ActionResult.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost(Name = "ThirdPartyMovies")]
    public async Task<ActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetThirdPartyMoviesCommand());
        return Ok(result);
    }
}