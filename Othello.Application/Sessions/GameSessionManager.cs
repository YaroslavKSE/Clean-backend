using MediatR;
using Othello.Application.GameInterfaces;
using Othello.Domain;

namespace Othello.Application.Sessions;

public class GameSessionManager : IGameSessionTurnChecker
{
    private readonly IGameRepository _gameRepository;
    private readonly IMediator _mediator;

    public GameSessionManager(IGameRepository gameRepository, IMediator mediator)
    {
        _gameRepository = gameRepository;
        _mediator = mediator;
    }

    public async Task CheckAndExecuteTurn(Guid gameId)
    {
        var session = await _gameRepository.GetGameSessionByIdAsync(gameId);
        if (session == null) throw new InvalidOperationException("Game session not found.");

        var currentPlayer = session.Game.CurrentPlayer;
        if (currentPlayer is AIBot aiBot)
        {
            // It's the AI's turn to make a move
            await aiBot.MakeMoveAsync(gameId, session.Game);
        }
    }
}