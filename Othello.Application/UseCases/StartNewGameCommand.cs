using MediatR;
using Othello.Application.Interfaces;

namespace Othello.Application.UseCases;

public class StartNewGameCommand : IRequest<StartNewGameResult>
{
    public string UserId { get; set; }
    public string OpponentType { get; set; } // "player" or "cpu"
    public string OpponentName { get; set; } // Optional
}

public class StartNewGameResult
{
    public bool GameStarted { get; set; }
    public Guid GameId { get; set; }
    public string Message { get; set; }
}

public class StartNewGameCommandHandler : IRequestHandler<StartNewGameCommand, StartNewGameResult>
{
    private readonly IGameCreator _gameCreationService;

    public StartNewGameCommandHandler(IGameCreator gameCreationService)
    {
        _gameCreationService = gameCreationService;
    }

    public async Task<StartNewGameResult> Handle(StartNewGameCommand request, CancellationToken cancellationToken)
    {
        var gameId = await _gameCreationService.CreateGame(request.UserId, request.OpponentType, request.OpponentName);
        
        return new StartNewGameResult
        {
            GameStarted = gameId != Guid.Empty,
            GameId = gameId,
            Message = gameId != Guid.Empty ? "Game started successfully." : "Failed to start game."
        };
    }
}