using MediatR;
using Othello.Application.GameInterfaces;
using Web.Domain;

namespace Othello.Application.UseCases;

public class SendChatMessageCommand : IRequest<ChatMessageResult>
{
    public Guid GameId { get; set; }
    public string Username { get; set; } 
    public string MessageText { get; set; }
}

public class SendChatMessageRequest
{
    public string MessageText { get; set; } 
}
public class ChatMessageResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
}

public class SendChatMessageCommandHandler : IRequestHandler<SendChatMessageCommand, ChatMessageResult>
{
    private readonly IGameRepository _gameRepository;

    public SendChatMessageCommandHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<ChatMessageResult> Handle(SendChatMessageCommand request, CancellationToken cancellationToken)
    {
        var session = await _gameRepository.GetGameSessionByIdAsync(request.GameId);
        if (session == null)
        {
            return new ChatMessageResult { Success = false, Message = "Game session not found." };
        }

        var username = request.Username;
        var chatMessage = new ChatMessage(request.GameId, username, request.MessageText);
        // Assuming you have a method to save chat messages
        await _gameRepository.SaveChatMessageAsync(chatMessage);

        return new ChatMessageResult { Success = true, Message = "Message sent successfully." };
    }
}