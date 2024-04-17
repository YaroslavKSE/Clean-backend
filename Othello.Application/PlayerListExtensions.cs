using Othello.Application.Sessions;

namespace Othello.Application;

public static class PlayerListExtensions
{
    public static PlayerInfo? FindByUserId(this List<PlayerInfo?> players, string username)
    {
        return players.FirstOrDefault(p => p != null && p.WebUsername == username);
    }
}