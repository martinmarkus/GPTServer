using GPTServer.Common.Core.Constants;
using GPTServer.Common.Core.ContextInfo;
using GPTServer.Web.Extensions;

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
        IContextInfo contextInfo)
    {
        contextInfo.UserId = Guid.TryParse(httpContext.GetFirstHeaderValueOrDefault(ContextInfoConstants.UserIdKey, ContextInfoConstants.UserIdValue), out Guid userId) ? userId : null;

		contextInfo.Email = httpContext.GetFirstHeaderValueOrDefault(ContextInfoConstants.EmailKey, ContextInfoConstants.EmailValue);
        contextInfo.AuthToken = httpContext.GetFirstHeaderValueOrDefault(ContextInfoConstants.AuthTokenKey, ContextInfoConstants.AuthTokenValue);
        contextInfo.ClientIP = httpContext.GetFirstHeaderValueOrDefault(ContextInfoConstants.ClientIPKey, ContextInfoConstants.ClientIPValue);
        contextInfo.UserAgent = httpContext.GetFirstHeaderValueOrDefault(ContextInfoConstants.UserAgentKey, ContextInfoConstants.UserAgentValue);
        contextInfo.Language = httpContext.GetFirstHeaderValueOrDefault(ContextInfoConstants.LanguageKey, ContextInfoConstants.LanguageValue);

		await _next(httpContext);
    }
}
