using MediatR;
using Othello.Application.GameInterfaces;
using Othello.Application.PlayerInterfaces;
using Othello.Application.Sessions;
using Othello.Domain;

namespace Othello.Application.UseCases;

public class JoinGameCommand : IRequest<JoinGameResult>
{
    public Guid GameId { get; set; }
    public string Username { get; set; }
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
        var session = await _gameRepository.GetGameSessionByIdAsync(request.GameId);
        if (session == null) return new JoinGameResult {GameJoined = false, Message = "Game session not found."};
        // Check if the session already has two players
        if (session.Players.Count >= 2)
            return new JoinGameResult {GameJoined = false, Message = "Game session is already full."};

        // Assuming the second player info is passed with the request or created here
        var secondPlayerInfo =
            new PlayerInfo(request.Username, new HumanPlayer(CellState.Black, new ApiPlayerInputGetter()));
        session.Players.Add(secondPlayerInfo);

        // If this is the second player joining, start the game
        if (session.Players.Count == 2)
        {
            session.StartGame(); // Start the game now that both players are present
            await _gameRepository.JoinGameSessionAsync(session.GameId,
                secondPlayerInfo); // Update the session in the repository
        }

        return new JoinGameResult
        {
            GameJoined = true,
            Message = "Joined game successfully. " +
                      (session.Players.Count == 2 ? "Game started." : "Waiting for another player.")
        };
    }
}