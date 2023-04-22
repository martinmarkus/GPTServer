using GPTServer.Common.Core.Configurations;
using GPTServer.Common.DomainLogic.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GPTServer.Common.DomainLogic.Services;

public class AuthTokenService : IAuthTokenService
{
    private readonly BaseOptions _baseOptions;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

    public AuthTokenService(
        IOptions<BaseOptions> options)
    {
        _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        _baseOptions = options.Value;
    }

    public IEnumerable<Claim> GetClaimsFromJwtToken(string jwtToken)
    {
        if (string.IsNullOrEmpty(jwtToken))
        {
            return default;
        }
        var refreshToken = new JwtSecurityTokenHandler().ReadJwtToken(jwtToken);
        return refreshToken.Claims;
    }

    public string GenerateJwtToken(IEnumerable<Claim> claims)
    {
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _baseOptions.AuthIssuer,
            Expires = DateTime.UtcNow.AddMinutes(Math.Abs(_baseOptions.AuthExpirationMinutes)),
            SigningCredentials = new SigningCredentials(
                   new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_baseOptions.AuthSecretKey)),
                   SecurityAlgorithms.HmacSha256Signature)
        };

        var token = _jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
        return _jwtSecurityTokenHandler.WriteToken(token);
    }

    public bool ValidateToken(string jwtToken)
    {
        try
        {
            new JwtSecurityTokenHandler()
                .ValidateToken(
                    token: jwtToken ?? string.Empty,
                    validationParameters: GetJwtValidationParameters(),
                    out _);
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }

    public TokenValidationParameters GetJwtValidationParameters() =>
        new()
        {
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_baseOptions.AuthSecretKey)),
            ValidateIssuer = false,
            ValidIssuer = _baseOptions.AuthIssuer,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero,
        };
}
