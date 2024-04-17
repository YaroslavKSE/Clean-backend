using MediatR;
using Othello.Application.UserInterfaces;
using Web.Domain;

namespace Othello.Application.UseCases;

public class RegisterUserCommand : IRequest<RegisterUserResult>
{
    public string Username { get; set; }
    public string Password { get; set;}
    public string Email { get; set; }
}

public class RegisterUserResult
{
    public bool UserExists { get; init; }
    public bool UserCreated { get; init; }
    public string? Message { get; set; }
}

public class RegisterUserUseCase : IRequestHandler<RegisterUserCommand, RegisterUserResult>
{
    private readonly IUserExistChecker _userExistChecker;

    private readonly IUserStorage _userStorage;

    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserUseCase(IUserExistChecker userExistChecker, IUserStorage userStorage,
        IPasswordHasher passwordHasher)
    {
        _userExistChecker = userExistChecker;
        _userStorage = userStorage;
        _passwordHasher = passwordHasher;
    }

    public async Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userExistChecker.ExistsAsync(request.Username))
            return new RegisterUserResult {UserExists = true, Message = "User already exists."};
        var hashedPassword = _passwordHasher.HashPassword(request.Password);
        var user = new User(request.Username, hashedPassword, request.Email);

        await _userStorage.AddAsync(user);

        return new RegisterUserResult {UserCreated = true, Message = "User created successfully."};
    }
}