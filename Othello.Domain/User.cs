namespace Othello.Domain;

public class User
{
    public Guid Id { get; private set; } // Unique identifier for the User.
    public string Username { get; private set; } // The user's chosen username.
    public string Email { get; private set; } // The user's email.
    public string PasswordHash { get; private set; } // The hashed password for security.

    public User(string username, string passwordHash, string email)
    {
        Id = Guid.NewGuid();
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
    }
}