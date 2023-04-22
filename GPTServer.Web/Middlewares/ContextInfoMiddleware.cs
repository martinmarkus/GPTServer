using GPTServer.Common.Core.Constants;
using GPTServer.Common.Core.ContextInfo;
using GPTServer.Common.DomainLogic.Interfaces;
using GPTServer.Web.Extensions;
using Org.BouncyCastle.Security;

namespace GPTServer.Web.Middlewares;

public class ContextInfoMiddleware
{
    private readonly RequestDelegate _next;

    public ContextInfoMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext httpContext,
        IContextInfo contextInfo,
        IAuthTokenService authTokenService)
    {
        string jwtToken = httpContext.GetFirstHeaderValueOrDefault(CookieConstants.AuthToken, string.Empty);
        if (!string.IsNullOrEmpty(jwtToken) && jwtToken.Contains(" "))
        {
            jwtToken = jwtToken.Split(" ")[1];
        }
        var claims = authTokenService.GetClaimsFromJwtToken(jwtToken);

        bool hasUserId = Guid.TryParse(claims?.FirstOrDefault(x => x.Type == ClaimConstants.Id)?.Value, out Guid userId);
        string email = claims?.FirstOrDefault(x => x.Type == ClaimConstants.Email)?.Value ?? string.Empty;

        contextInfo.SetValues(
            userId: hasUserId ? userId : null,
            email: email,
            authToken: jwtToken,
            authRoutingEnvironment: httpContext.GetFirstHeaderValueOrDefault(ContextInfoConstants.AuthRoutingEnvKey, ContextInfoConstants.AuthRoutingEnvValue),
            clientIP: httpContext.Connection?.RemoteIpAddress?.ToString() ?? string.Empty,
            userAgent: httpContext.GetFirstHeaderValueOrDefault(ContextInfoConstants.UserAgentKey, ContextInfoConstants.UserAgentValue),
            language: httpContext.GetFirstHeaderValueOrDefault(ContextInfoConstants.LanguageKey, ContextInfoConstants.LanguageValue)
        );

        await _next(httpContext);
    }
}
