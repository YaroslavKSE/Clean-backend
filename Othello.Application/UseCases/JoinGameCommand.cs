using MediatR;
using System;
using Othello.Application.Interfaces;

namespace Clean.Application.UseCases;

public class JoinGameCommand : IRequest<JoinGameResult>
{
    public Guid GameId { get; set; }
    public string UserId { get; set; }
}

public class JoinGameResult
{
    public bool GameJoined { get; set; }
    public string Message { get; set; }
}

public class JoinGameCommandHandler : IRequestHandler<JoinGameCommand, JoinGameResult>
{
    private readonly IGameRepository _gameRepository;

    public JoinGameCommandHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<JoinGameResult> Handle(JoinGameCommand request, CancellationToken cancellationToken)
    {
        // Assuming JoinGameAsync now updates a GameSession object
        var sessionUpdated = await _gameRepository.JoinGameSessionAsync(request.GameId, request.UserId);
    
        return new JoinGameResult
        {
            GameJoined = sessionUpdated,
            Message = sessionUpdated ? "Joined game successfully." : "Failed to join game."
        };
    }
}