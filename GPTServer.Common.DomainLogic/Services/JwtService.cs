using GPTServer.Common.Core.Configurations;
using GPTServer.Common.DomainLogic.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GPTServer.Common.DomainLogic.Services;
public class JwtService : IJwtService
{
	private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
	private readonly BaseOptions _baseOptions;

	public JwtService(
		JwtSecurityTokenHandler jwtSecurityTokenHandler,
		IOptions<BaseOptions> baseOptions)
	{
		_jwtSecurityTokenHandler = jwtSecurityTokenHandler;
		_baseOptions = baseOptions.Value;
	}

	public string GenerateJwt(IList<Claim> claims)
	{
		var tokenDescriptor = new SecurityTokenDescriptor()
		{
			Subject = new ClaimsIdentity(claims),
			Issuer = _baseOptions.AuthIssuer,
			SigningCredentials = new SigningCredentials(
				new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_baseOptions.AuthSecretKey)),
				SecurityAlgorithms.HmacSha256Signature),
			Expires = DateTime.Now.AddMinutes(Math.Abs(_baseOptions.AuthExpirationMinutes))
		};

		string jwt = string.Empty;

		try
		{
			var token = _jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
			jwt = _jwtSecurityTokenHandler.WriteToken(token);
		}
		catch (Exception e)
		{
            Console.WriteLine(e);
        }

		return jwt;
	}
}
