namespace Web.Domain;

public class GameStatistics
{
    public Guid UserId { get; private set; } 
    public int TotalGamesPlayed { get; private set; }
    public int Wins { get; private set; }
    public int Losses { get; private set; }
    public int Draws { get; private set; } 

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

   
}