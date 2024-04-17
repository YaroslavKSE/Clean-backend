using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Othello.Application.UserInterfaces;
using Web.Domain;

namespace Othello.Infrastructure.UserServices;

public class TokenGenerator : ITokenGenerator
{
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;

    public TokenGenerator(string secretKey, string issuer, string audience)
    {
        _secretKey = secretKey;
        _issuer = issuer;
        _audience = audience;
    }

    public string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}