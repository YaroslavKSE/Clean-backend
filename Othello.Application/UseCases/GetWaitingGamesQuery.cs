using MediatR;
using Othello.Application.GameInterfaces;

namespace Othello.Application.UseCases;

public class GetWaitingGamesQuery : IRequest<IEnumerable<GameInfo>>
{
}

public class GameInfo
{
    public Guid GameId { get; set; }
    public string PlayerUsername { get; set; } // Username of the player who is waiting
}

public class GetWaitingGamesQueryHandler : IRequestHandler<GetWaitingGamesQuery, IEnumerable<GameInfo>>
{
    private readonly IGameRepository _gameRepository;

    public GetWaitingGamesQueryHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<IEnumerable<GameInfo>> Handle(GetWaitingGamesQuery request, CancellationToken cancellationToken)
    {
        // Retrieve waiting game sessions
        var waitingGameSessions = await _gameRepository.GetWaitingGameSessionsAsync();

        // Transform the collection of GameSession objects to GameInfo DTOs
        var waitingGamesInfo = waitingGameSessions.Select(session => new GameInfo
        {
            GameId = session.GameId,
            PlayerUsername = session.Players.FirstOrDefault()?.WebUsername ?? "Waiting Player"
        });

        return waitingGamesInfo;
    }
}