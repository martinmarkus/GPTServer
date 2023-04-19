using System.Security.Claims;

namespace GPTServer.Common.DomainLogic.Interfaces;
public interface IJwtService
{
	string GenerateJwt(IList<Claim> claims);
}
