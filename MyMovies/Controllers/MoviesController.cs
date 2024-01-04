using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyMovies.Handlers;
using MyMovies.Models.RequestsBody;

namespace MyMovies.Controllers;

[ApiController]
[Route("movies")]
public class MoviesController : ControllerBase
{
    private readonly IMediator _mediator;

    public MoviesController(
        IMediator mediator
        )
    {
        _mediator = mediator;
    }
    [HttpGet(Name = "GetMovies")]
    public async Task<ActionResult> GetAll()
    {
        var result  = await _mediator.Send(new GetMoviesQuery());
        return Ok(result);
    }
    
    [HttpGet("{id}", Name = "GetMovieById")]
    public async Task<ActionResult> GetById(
        [FromRoute] int id
        )
    {
        var result = await _mediator.Send(new GetMovieByIdQuery(id));
        return Ok(result);
    }    
    [HttpPost(Name = "PostMovie")]
    public async Task<ActionResult> Post(
        [FromBody] AddMovieCommand command)
    {
        var createdMovie = await _mediator.Send(command);
        return Created($"/api/Donors/{createdMovie.Id}", createdMovie);
    }
    
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
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _mediator.Send(new DeleteMovieCommand(id));
        return NoContent();
    }

}