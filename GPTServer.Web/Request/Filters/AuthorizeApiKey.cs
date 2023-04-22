using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using GPTServer.Common.Core.Configurations;
using Microsoft.Extensions.Options;
using GPTServer.Common.Core.Constants;
using GPTServer.Web.Extensions;

namespace GPTServer.Web.Request.Filters;

public class AuthorizeApiKey : ActionFilterAttribute
{
    private readonly BaseOptions _baseOptions;

    public AuthorizeApiKey(IOptions<BaseOptions> baseOptions)
    {
        _baseOptions = baseOptions.Value;
    }

    public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        string apiKey = context.HttpContext.GetFirstHeaderValueOrDefault(CookieConstants.ApiKey, string.Empty);

        // INFO: Api key missing/mismatch
        if (string.IsNullOrEmpty(apiKey)
            || !string.Equals(apiKey.Trim(), _baseOptions.ApiKey.Trim(), StringComparison.OrdinalIgnoreCase))
        {
            context.Result = new ForbidResult();
            return;
        }

        await next();
    }
}