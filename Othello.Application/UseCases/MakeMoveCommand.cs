using MediatR;
using Othello.Application.GameInterfaces;
using Othello.Application.PlayerInterfaces;

namespace Othello.Application.UseCases;

public class MakeMoveCommand : IRequest<MakeMoveResult>
{
    public Guid GameId { get; set; }
    public string UserId { get; set; }
}

public class MakeMoveResult
{
    public bool IsValid { get; set; }
    public string Message { get; set; }
}

public class MakeMoveCommandHandler : IRequestHandler<MakeMoveCommand, MakeMoveResult>
{
    private readonly IGameRepository _gameRepository;
    private readonly ApiPlayerInputGetter _inputGetter;

    public MakeMoveCommandHandler(IGameRepository gameRepository, ApiPlayerInputGetter inputGetter)
    {
        _gameRepository = gameRepository;
        _inputGetter = inputGetter;
    }

    public async Task<MakeMoveResult> Handle(MakeMoveCommand request, CancellationToken cancellationToken)
    {
        var gameSession = await _gameRepository.GetGameSessionByIdAsync(request.GameId);
        if (gameSession == null)
        {
            return new MakeMoveResult { IsValid = false, Message = "Game not found." };
        }
        // Wait for move input via ApiPlayerInputGetter
        var (row, column) = await _inputGetter.WaitForMoveAsync(request.GameId);
        var moveMade = gameSession.MakeMove(row, column);
        if (!moveMade)
        {
            return new MakeMoveResult { IsValid = false, Message = "Invalid move." };
        }

        return new MakeMoveResult { IsValid = true, Message = "Move made successfully." };
    }
}