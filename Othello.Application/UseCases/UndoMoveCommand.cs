using MediatR;
using Othello.Application.GameInterfaces;
using Othello.Application.PlayerInterfaces;
using Othello.Domain.Interfaces;

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
    private readonly IUndoRequestListener _undoRequestListener;

    public UndoMoveCommandHandler(IGameRepository gameRepository, IUndoRequestListener undoRequestListener)
    {
        _gameRepository = gameRepository;
        _undoRequestListener = undoRequestListener;
    }

    public async Task<UndoMoveResult> Handle(UndoMoveCommand request, CancellationToken cancellationToken)
    {
        var gameSession = await _gameRepository.GetGameSessionByIdAsync(request.GameId);
        if (gameSession == null) return new UndoMoveResult {MoveUndone = false, Message = "Game not found."};

        // Wait for undo request via ApiUndoRequestListener
        var undoRequested = await _undoRequestListener.WaitForUndoAsync(request.GameId);
        if (!undoRequested) return new UndoMoveResult {MoveUndone = false, Message = "Undo request not received."};

        var canUndo = gameSession.UndoMove();
        if (!canUndo) return new UndoMoveResult {MoveUndone = false, Message = "Cannot undo move."};

        return new UndoMoveResult {MoveUndone = true, Message = "Move undone successfully."};
    }
}