﻿using MediatR;
using Othello.Application.GameInterfaces;
using Othello.Application.PlayerInterfaces;

namespace Othello.Application.UseCases;

public class MakeMoveCommand : IRequest<MakeMoveResult>
{
    public Guid GameId { get; set; }
    public string Username { get; set; }
    
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
    private readonly ApiPlayerInputGetter _inputGetter;

    public MakeMoveCommandHandler(IGameRepository gameRepository, ApiPlayerInputGetter inputGetter)
    {
        _gameRepository = gameRepository;
        _inputGetter = inputGetter;
    }

    public async Task<MakeMoveResult> Handle(MakeMoveCommand request, CancellationToken cancellationToken)
    {
        var session = await _gameRepository.GetGameSessionByIdAsync(request.GameId);
        if (session == null)
        {
            return new MakeMoveResult { IsValid = false, Message = "Game session not found." };
        }

        var currentPlayer = session.Players.FindByUserId(request.Username);
        if (session.Game.CurrentPlayer != currentPlayer?.OthelloPlayer) 
        {
            return new MakeMoveResult { IsValid = false, Message = "Not your turn." };
        }

        await currentPlayer.OthelloPlayer.MakeMoveAsync(request.GameId, session.Game);
        return new MakeMoveResult { IsValid = true, Message = "Move made successfully." };
    }
}