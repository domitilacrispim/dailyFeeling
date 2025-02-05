using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DailyFeeling.Models;
using Microsoft.IdentityModel.Tokens;

namespace DailyFeeling.Services;

public class JwtService
{
    private readonly string? _secretKey;
    private readonly int _tokenExpirationInHours;

    public JwtService(IConfiguration configuration)
    {
        _secretKey = configuration["Jwt:SecretKey"];
        _tokenExpirationInHours = int.Parse(configuration["Jwt:TokenExpirationInHours"]);
    }

    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("username", user.Username),
                new Claim("userId", user.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(_tokenExpirationInHours),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    
}