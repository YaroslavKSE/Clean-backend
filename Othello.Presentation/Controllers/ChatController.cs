using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Othello.Application.UseCases;

namespace Othello.Presentation.Controllers;

[ApiController]
[Route("api/chat")]
[Authorize]
public class ChatController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChatController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("{gameId}/send")]
    public async Task<IActionResult> SendChatMessage(Guid gameId, [FromBody] SendChatMessageRequest request)
    {
        string username = User.Identity?.Name;
        if (string.IsNullOrEmpty(username))
        {
            return Unauthorized("User is not authenticated.");
        }

        var command = new SendChatMessageCommand
        {
            GameId = gameId,
            Username = username,
            MessageText = request.MessageText
        };

        var result = await _mediator.Send(command);
        return result.Success ? Ok(result) : BadRequest(result.Message);
    }
    
    [HttpGet("{gameId}/get")]
    [Authorize] // Ensure only authorized users can access
    public async Task<IActionResult> GetChatMessages(Guid gameId)
    {
        var query = new GetChatMessagesQuery { GameId = gameId };
        var messages = await _mediator.Send(query);

        return Ok(messages);
    }
}