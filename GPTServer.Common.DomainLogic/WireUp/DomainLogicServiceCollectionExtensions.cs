using GPTServer.Common.DomainLogic.Interfaces;
using GPTServer.Common.DomainLogic.Services;
using GPTServer.Common.Utils.GPTClient.WireUp;
using Microsoft.Extensions.DependencyInjection;

namespace GPTServer.Common.DomainLogic.WireUp;

public static class DomainLogicServiceCollectionExtensions
{
    public static void AddDomainLogic(this IServiceCollection services)
    {
        services.AddScoped<IGPTService, GPTService>();
        services.AddScoped<ILogService, LogService>();
        services.AddScoped<IAuthTokenService, AuthTokenService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ISecureHashGeneratorService, SecureHashGeneratorService>();

        services.AddGPTClient();
    }
}
