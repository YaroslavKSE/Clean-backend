using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Othello.Application.UseCases;

namespace Othello.Presentation.Controllers;


[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{username}/stats")]
    [Authorize] // Ensure only authorized users can access their stats
    public async Task<IActionResult> GetUserStats(string username)
    {
        var query = new GetUserStatsQuery { Username = username };
        var result = await _mediator.Send(query);

        return result != null ? Ok(result) : NotFound("User statistics not found.");
    }
}