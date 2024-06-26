﻿using MediatR;
using Othello.Application.GameInterfaces;

namespace Othello.Application.UseCases;

public class StartNewGameCommand : IRequest<StartNewGameResult>
{
    public string Username { get; init; }
    public string OpponentType { get; set; } // "player" or "cpu"

}
public class StartNewGameRequest
{
    public string OpponentType { get; set; } // "player" or "cpu"
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
        
        // Create a new GameSession with the appropriate player and opponent
        var gameSession =
            await _gameCreationService.CreateGameSession(request.Username, request.OpponentType);

        // If the opponent is a bot, start the game immediately
        if (request.OpponentType.ToLower() == "bot")
        {
            gameSession.StartGame(); // This method would start the game
            return new StartNewGameResult
            {
                GameStarted = true,
                GameId = gameSession.GameId,
                Message = "Game started successfully with a bot."
            };
        }

        // If the opponent is another player, the game waits for another player to join
        return new StartNewGameResult
        {
            GameStarted = false, // Game not started yet, waiting for another player
            GameId = gameSession.GameId,
            Message = "Game session created. Waiting for another player to join."
        };
    }
}