namespace Othello.Domain;

public class MoveEventArgs : EventArgs
{
    public int X { get; }
    public int Y { get; }
    public Player Player { get; }

    public MoveEventArgs(int x, int y, Player player)
    {
        X = x;
        Y = y;
        Player = player;
    }
}