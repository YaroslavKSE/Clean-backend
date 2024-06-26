﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Othello.Application.Sessions;
using Othello.Application.UseCases;
using Othello.Domain.Interfaces;
using Othello.Presentation.RequestDTO;

namespace Othello.Presentation.Controllers;

[ApiController]
[Route("api/games")]
[Authorize]
public class GameController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IPlayerInputGetter _inputGetter;
    private readonly IUndoRequestListener _undoRequestListener;
    

    public GameController(IMediator mediator, IPlayerInputGetter inputGetter,
        IUndoRequestListener undoRequestListener)
    {
        _mediator = mediator;
        _inputGetter = inputGetter;
        _undoRequestListener = undoRequestListener;
    }
    
    [HttpPost("new")]
    public async Task<IActionResult> StartNewGame([FromBody] StartNewGameRequest request)
    {
        string username = User.Identity?.Name; // Get the username from the token
        if (string.IsNullOrEmpty(username))
        {
            return Unauthorized("User is not authenticated.");
        }

        var command = new StartNewGameCommand
        {
            Username = username,
            OpponentType = request.OpponentType
        };
        var result = await _mediator.Send(command);
        
        if (result.GameId != Guid.Empty)
        {
            if (result.GameStarted)
                return StatusCode(201, new {result.GameId, Message = "Game started successfully with the bot."});

            return StatusCode(201,
                new {result.GameId, Message = "Game session created. Waiting for another player to join."});
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
    public async Task<IActionResult> JoinGame([FromBody] JoinGameRequest request)
    {
        string username = User.Identity?.Name;
        if (string.IsNullOrEmpty(username))
        {
            return Unauthorized("User is not authenticated.");
        }
        
        var command = new JoinGameCommand
        {
            GameId = request.GameId,
            Username = username
        };
        
        var result = await _mediator.Send(command);
        if (result.GameJoined) return Ok(new {result.Message});

        return BadRequest(result.Message);
    }


    [HttpPost("{gameId}/move")]
    public async Task<IActionResult> MakeMove([FromRoute] Guid gameId, [FromBody] MoveRequest move)
    {
        // Get the username from the claims
        string username = User.Identity?.Name;

        if (string.IsNullOrEmpty(username))
        {
            return BadRequest("User is not recognized.");
        }
        
        var command = new MakeMoveCommand
        {
            GameId = gameId,
            Username = username,
            Row = move.Row,
            Column = move.Column
        };
        var result = await _mediator.Send(command);
        if (result.IsValid)
        {
            return Ok(result);
        }

        return BadRequest(result.Message);
        
    }


    [HttpPost("{gameId}/undo")]
    public async Task<IActionResult> RequestUndo(Guid gameId)
    {
        // Trigger the undo request
        _undoRequestListener.RequestUndo(gameId);

        // Now call the UndoMoveCommand to handle the actual undo logic
        var command = new UndoMoveCommand {GameId = gameId};
        var result = await _mediator.Send(command);

        // Return appropriate response based on the result of the undo operation
        return result.MoveUndone ? Ok(result) : BadRequest(result.Message);
    }

    [HttpGet("{gameId}/hint")]
    public async Task<IActionResult> GetHints([FromRoute] Guid gameId)
    {
        var result = await _mediator.Send(new GetHintsQuery {GameId = gameId});
        return Ok(result);
    }

}