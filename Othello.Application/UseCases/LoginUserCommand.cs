using MediatR;
using Othello.Application.Interfaces;

namespace Othello.Application.UseCases;

public class LoginUserCommand : IRequest<LoginUserResult>
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginUserResult
{
    public bool UserAuthenticated { get; set; }
    public string Token { get; set; }
}

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResult>
{
    private readonly IUserStorage _userStorage;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenGenerator _tokenGenerator;

    public LoginUserCommandHandler(IUserStorage userStorage, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator)
    {
        _userStorage = userStorage;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<LoginUserResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userStorage.FindByUsernameAsync(request.Username);

        if (user == null || !_passwordHasher.VerifyPassword(user.PasswordHash, request.Password))
        {
            return new LoginUserResult { UserAuthenticated = false };
        }

        var token = _tokenGenerator.GenerateToken(user);

        return new LoginUserResult { UserAuthenticated = true, Token = token };
    }
}