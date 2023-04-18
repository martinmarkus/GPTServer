using GPTServer.Common.Core.ContextInfo;

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
        // INFO: Fill ContextInfo
        await _next(httpContext);
    }
}
