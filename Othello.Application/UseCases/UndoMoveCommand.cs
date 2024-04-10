using MediatR;
using Othello.Application.Interfaces;

namespace Othello.Application.UseCases;

public class UndoMoveCommand : IRequest<UndoMoveResult>
{
    public Guid GameId { get; set; }
}

public class UndoMoveResult
{
    public bool MoveUndone { get; set; }
    public string Message { get; set; }
}

public class UndoMoveCommandHandler : IRequestHandler<UndoMoveCommand, UndoMoveResult>
{
    private readonly IGameRepository _gameRepository;

    public UndoMoveCommandHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<UndoMoveResult> Handle(UndoMoveCommand request, CancellationToken cancellationToken)
    {
        var game = await _gameRepository.GetGameByIdAsync(request.GameId);
        if (game == null)
        {
            return new UndoMoveResult { MoveUndone = false, Message = "Game not found." };
        }

        // Assuming Game has a method to handle undo that returns a boolean indicating success/failure
        var canUndo = game.UndoMove();
        if (!canUndo)
        {
            return new UndoMoveResult { MoveUndone = false, Message = "Cannot undo move." };
        }

        return new UndoMoveResult { MoveUndone = true, Message = "Move undone successfully." };
    }
}