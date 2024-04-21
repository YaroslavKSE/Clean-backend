using MediatR;
using Othello.Application.GameInterfaces;

namespace Othello.Application.UseCases;

public class GetChatMessagesQuery : IRequest<List<ChatMessageDto>>
{
    public Guid GameId { get; set; }
}

public class ChatMessageDto
{
    public string SenderUsername { get; set; }
    public string MessageText { get; set; }
    public DateTime Timestamp { get; set; }
}

public class GetChatMessagesQueryHandler : IRequestHandler<GetChatMessagesQuery, List<ChatMessageDto>>
{
    private readonly IGameRepository _gameRepository;

    public GetChatMessagesQueryHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<List<ChatMessageDto>> Handle(GetChatMessagesQuery request, CancellationToken cancellationToken)
    {
        var messages = await _gameRepository.GetChatMessagesBySessionIdAsync(request.GameId);
        return messages.Select(msg => new ChatMessageDto
        {
            SenderUsername = msg.Sender, 
            MessageText = msg.MessageText,
            Timestamp = msg.Timestamp
        }).ToList();
    }
}