#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyMovies.Handlers;
using MyMovies.Models.RequestsBody;

#endregion

namespace MyMovies.Controllers;

[ApiController]
[Route("api/movies")]
public class MoviesController : ControllerBase
{
    private readonly IMediator _mediator;

    public MoviesController(
        IMediator mediator
    )
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Retrieves all movies from the database.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the ActionResult.</returns>
    [HttpGet(Name = "GetMovies")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetMoviesQuery());
        return Ok(result);
    }

    /// <summary>
    ///     Retrieves a movie by id.
    ///     if the movie does not exist, returns a 404.
    /// </summary>
    /// <returns> A task that represents the asynchronous operation. The task result contains the ActionResult.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}", Name = "GetMovieById")]
    public async Task<ActionResult> GetById(
        [FromRoute] int id
    )
    {
        var result = await _mediator.Send(new GetMovieByIdQuery(id));
        return Ok(result);
    }

    /// <summary>
    ///     Adds a movie to the database if validation succeeds.
    ///     returns a 201 if the movie is added successfully, 400 otherwise.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the ActionResult.</returns>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost(Name = "PostMovie")]
    public async Task<ActionResult> Post(
        [FromBody] AddMovieCommand command)
    {
        var createdMovie = await _mediator.Send(command);
        return Created($"/api/Donors/{createdMovie.Id}", createdMovie);
    }


    /// <summary>
    ///     Updates a movie if validation succeeds.
    ///     returns a 200 if the movie is updated successfully, 404 if movie is not found, otherwise 400.
    /// </summary>
    /// <returns> A task that represents the asynchronous operation. The task result contains the ActionResult.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(
        [FromRoute] int id, [FromBody] UpdateMovieRequestBody requestBody)
    {
        var command = new UpdateMovieCommand
        {
            Id = id,
            Title = requestBody.Title,
            Director = requestBody.Director,
            Year = requestBody.Year,
            Rate = requestBody.Rate
        };
        var dto = await _mediator.Send(command);
        return Ok(dto);
    }

    /// <summary>
    ///     Deletes a movie by id if it exists.
    ///     returns a 204 if the movie is deleted successfully, 404 if movie is not found, otherwise 400.
    /// </summary>
    /// <returns> A task that represents the asynchronous operation. The task result contains the ActionResult.</returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _mediator.Send(new DeleteMovieCommand(id));
        return NoContent();
    }
}