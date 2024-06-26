﻿namespace Web.Domain;

public class GameStatistics
{
    public Guid UserId { get; private set; }
    public int TotalGamesPlayed { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
    public int Draws { get; set; } 

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