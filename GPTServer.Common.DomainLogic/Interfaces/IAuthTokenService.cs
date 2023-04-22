using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace GPTServer.Common.DomainLogic.Interfaces;
public interface IAuthTokenService
{
    string GenerateJwtToken(IEnumerable<Claim> claims);

    IEnumerable<Claim> GetClaimsFromJwtToken(string jwtToken);

    TokenValidationParameters GetJwtValidationParameters();

    bool ValidateToken(string jwtToken);
}
