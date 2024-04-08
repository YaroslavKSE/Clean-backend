namespace Othello.Domain.Interfaces;

public interface IPlayerInputGetter
{
    (int, int) GetMoveInput();
}