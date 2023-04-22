using GPTServer.Common.Core.Constants;
using GPTServer.Common.Core.ContextInfo;
using GPTServer.Common.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GPTServer.Web.Request.Filters;

public class ExtensionPermission : ActionFilterAttribute
{
    private readonly IUserRepo _userRepo;
    private readonly IContextInfo _contextInfo;

    public ExtensionPermission(
        IUserRepo userRepo,
        IContextInfo contextInfo)
    {
        _userRepo = userRepo;
        _contextInfo = contextInfo;
    }

    public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var user = await _userRepo.GetByEmailAsync(_contextInfo.Email);
        if (user is null || !user.HasExtensionPermission)
        {
            context.Result = new ForbidResult();
            return;
        }

        await next();
    }
}