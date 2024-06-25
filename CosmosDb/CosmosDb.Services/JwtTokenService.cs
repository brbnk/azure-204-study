using System.IdentityModel.Tokens.Jwt;
using System.Text;
using CosmodeDb.Domain.Account;
using CosmosDb.Domain.Security.Interfaces;
using CosmosDb.Domain.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CosmosDb.Services;

public sealed class JwtTokenService(IOptions<JwtSettings> jwtSettings) : ITokenService
{
    public string Create(User user)
    {   
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Value.PrivateKey));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = user.GenerateClaims();

        var tokenDescriptor = new SecurityTokenDescriptor() 
        {
          SigningCredentials = credentials,
          Expires = DateTime.UtcNow.AddHours(jwtSettings.Value.ExpirationInHours),
          Subject = claims
        };

        var handler = new JwtSecurityTokenHandler();

        var token = handler.CreateToken(tokenDescriptor);

        return handler.WriteToken(token);
    }
}