using Web.Domain;

namespace Othello.Application.Interfaces;

public interface ITokenGenerator
{
    /// <summary>
    /// Creates a new token for a given user.
    /// </summary>
    /// <param name="user">The user for whom the token is being created.</param>
    /// <returns>A token string that can be used for authentication.</returns>
    string GenerateToken(User user);
}