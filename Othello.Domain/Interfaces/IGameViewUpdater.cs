namespace Othello.Domain.Interfaces;

public interface IGameViewUpdater
{
    void Update(string message);
    void DisplayBoard(CellState[,] board, List<(int, int)>? hints);
}