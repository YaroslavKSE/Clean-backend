using MediatR;
using Othello.Application.Interfaces;
using Othello.Domain;

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
        // Logic to retrieve games that are waiting for an opponent
        var waitingGames = await _gameRepository.GetWaitingGamesAsync();
        
        // Transform the collection of Game objects to GameInfo DTOs
        var waitingGamesList = waitingGames.ToList();
        var gameInfos = waitingGamesList.Select(game => new GameInfo
        {
            GameId = game.Id, // Assuming you add an Id property to your Game class
            PlayerUsername = game.CurrentPlayer.Color.ToString() // Assuming CurrentPlayer has a Username property
        });
        
        return gameInfos;
    }
}