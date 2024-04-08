namespace Othello.Domain;

public class GameStatistics
{
    public Guid UserId { get; private set; } // The identifier of the User these statistics belong to.
    public int TotalGamesPlayed { get; private set; }
    public int Wins { get; private set; }
    public int Losses { get; private set; }
    public int Draws { get; private set; } // If draws are possible in your game.

    public GameStatistics(Guid userId)
    {
        UserId = userId;
    }

    public void RecordWin()
    {
        TotalGamesPlayed++;
        Wins++;
    }

    public void RecordLoss()
    {
        TotalGamesPlayed++;
        Losses++;
    }

    public void RecordDraw()
    {
        TotalGamesPlayed++;
        Draws++;
    }

    // Additional methods to modify and access the statistics could be added here.
}
