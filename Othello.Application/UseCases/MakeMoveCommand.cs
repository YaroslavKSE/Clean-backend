using MediatR;
using Othello.Application.Interfaces;

namespace Othello.Application.UseCases;

public class MakeMoveCommand : IRequest<MakeMoveResult>
{
    public Guid GameId { get; set; }
    public string UserId { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
}

public class MakeMoveResult
{
    public bool IsValid { get; set; }
    public string Message { get; set; }
}

public class MakeMoveCommandHandler : IRequestHandler<MakeMoveCommand, MakeMoveResult>
{
    private readonly IGameRepository _gameRepository;

    public MakeMoveCommandHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<MakeMoveResult> Handle(MakeMoveCommand request, CancellationToken cancellationToken)
    {
        var game = await _gameRepository.GetGameByIdAsync(request.GameId);
        if (game == null)
        {
            return new MakeMoveResult { IsValid = false, Message = "Game not found." };
        }
        
        var moveMade = game.MakeMove(request.Row, request.Column);
        if (!moveMade)
        {
            return new MakeMoveResult { IsValid = false, Message = "Invalid move." };
        }

        return new MakeMoveResult { IsValid = true, Message = "Move made successfully." };
    }
}