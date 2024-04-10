using Clean.Application.UseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Othello.Application.UseCases;

namespace Othello.Presentation.Controllers;

[ApiController]
[Route("api/games")]
public class GameController : ControllerBase
{
    private readonly IMediator _mediator;

    public GameController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("new")]
    public async Task<IActionResult> StartNewGame(StartNewGameCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.GameStarted)
        {
            return StatusCode(201, result);
        }

        return BadRequest("Could not start a new game.");
    }
    
    [HttpGet("waiting")]
    public async Task<IActionResult> GetWaitingGames()
    {
        var result = await _mediator.Send(new GetWaitingGamesQuery());
        return Ok(result);
    }
    
    [HttpPost("join")]
    public async Task<IActionResult> JoinGame(JoinGameCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.GameJoined)
        {
            return Ok(result);
        }
    
        return BadRequest("Could not join the game.");
    }
    
    [HttpPost("{gameId}/move")]
    public async Task<IActionResult> MakeMove([FromRoute] Guid gameId, MakeMoveCommand command)
    {
        command.GameId = gameId;
        var result = await _mediator.Send(command);
        return result.IsValid ? (IActionResult)Ok(result) : BadRequest("Invalid move.");
    }
    
    [HttpPost("{gameId}/undo")]
    public async Task<IActionResult> UndoMove([FromRoute] Guid gameId, UndoMoveCommand command)
    {
        command.GameId = gameId;
        var result = await _mediator.Send(command);
        return result.MoveUndone ? (IActionResult)Ok("Move undone.") : BadRequest("Cannot undo move.");
    }
    
    [HttpGet("{gameId}/hint")]
    public async Task<IActionResult> GetHints([FromRoute] Guid gameId)
    {
        var result = await _mediator.Send(new GetHintsQuery { GameId = gameId });
        return Ok(result);
    }
    //
    // [HttpPost("{gameId}/chat")]
    // public async Task<IActionResult> SendChatMessage([FromRoute] Guid gameId, SendChatMessageCommand command)
    // {
    //     command.GameId = gameId;
    //     var result = await _mediator.Send(command);
    //     return result.MessageSent ? (IActionResult)Ok("Message sent.") : BadRequest("Could not send the message.");
    // }

    // Add more command and query handlers as needed for actions like getting game statistics, handling move timeouts, etc.
}