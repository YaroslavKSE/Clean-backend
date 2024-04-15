using Othello.Application.Interfaces;
using Othello.Domain;

namespace Othello.Infrastructure.GameServices;

public class GameCreator : IGameCreator
{
    public async Task<Guid> CreateGame(string userId, string opponentType, string? opponentName = null)
    {
        var user = await InMemoryDatabase.GetUserByUsernameAsync(userId);
        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }
        
        var player1 = new HumanPlayer(CellState.Black, ); 
        Player opponent;
        
        if (opponentType.ToLower() == "bot")
        {
            opponent = new AIBot(CellState.White);
        }
        else
        {
            opponent = new HumanPlayer(CellState.White); 
        }
        
        var game = new Game(player1, opponent, null);
        var gameId = await InMemoryDatabase.AddGameAsync(game);
        
        return gameId;
    }
}