using MediatR;
using Microsoft.AspNetCore.Mvc;
using Othello.Application.PlayerInterfaces;
using Othello.Application.UseCases;

namespace Othello.Presentation.Controllers;

[ApiController]
[Route("api/games")]
public class GameController : ControllerBase
{
    private readonly IMediator _mediator;
    private ApiPlayerInputGetter _inputGetter;
    private ApiUndoRequestListener _undoRequestListener;

    public GameController(IMediator mediator, ApiPlayerInputGetter inputGetter, ApiUndoRequestListener undoRequestListener)
    {
        _mediator = mediator;
        _inputGetter = inputGetter;
        _undoRequestListener = undoRequestListener;
    }
    
    [HttpPost("new")]
    public async Task<IActionResult> StartNewGame(StartNewGameCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.GameId != Guid.Empty)
        {
            if (result.GameStarted)
            {
                return StatusCode(201, new { result.GameId, Message = "Game started successfully with the bot." });
            }

            return StatusCode(201, new { result.GameId, Message = "Game session created. Waiting for another player to join." });
        }

        return BadRequest("Failed to create game session.");
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
            return Ok(new {result.Message });
        }

        return BadRequest(result.Message);
    }

    
    [HttpPost("{gameId}/move")]
    public IActionResult MakeMove(Guid gameId, [FromBody] int row, int col)
    {
        _inputGetter.SetMove(gameId, row, col);
        return Ok();
    }
    
    [HttpPost("{gameId}/undo")]
    public IActionResult RequestUndo(Guid gameId)
    {
        // Set undo in the undo service
        _undoRequestListener.RequestUndo(gameId);
        return Ok("Undo request submitted.");
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