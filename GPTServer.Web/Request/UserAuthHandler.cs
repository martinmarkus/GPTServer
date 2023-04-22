using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace GPTServer.Web.Request;

public class UserAuthHandler : AuthenticationHandler<UserAuthSchemeOptions>
{
	public UserAuthHandler(
		IOptionsMonitor<UserAuthSchemeOptions> options,
		ILoggerFactory logger,
		UrlEncoder encoder,
		ISystemClock clock)
		: base(options, logger, encoder, clock)
	{
	}

	protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
	{
		try
		{

			var claims = new List<Claim>();
			// TODO: get user claims
			var identity = new ClaimsIdentity(claims, nameof(UserAuthHandler));
			var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), Scheme.Name);

			return AuthenticateResult.Success(ticket);
		}
		catch
		{
			return AuthenticateResult.Fail("Exception during user authentication ");
		}
	}
}
