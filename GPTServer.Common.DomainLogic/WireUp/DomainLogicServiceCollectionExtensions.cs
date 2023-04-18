using GPTServer.Common.DomainLogic.Interfaces;
using GPTServer.Common.DomainLogic.Services;
using GPTServer.Common.Utils.GPTClient.WireUp;
using Microsoft.Extensions.DependencyInjection;

namespace GPTServer.Common.DomainLogic.WireUp;

public static class DomainLogicServiceCollectionExtensions
{
    /// <summary>
    /// Registers essential web dependencies, utils and logging service.
    /// </summary>
    /// <param name="services"></param>
    public static void AddDomainLogic(this IServiceCollection services)
    {
        services.AddScoped<IGPTService, GPTService>();

        services.AddGPTClient();
    }
}
