using GPTServer.Common.Utils.GPTClient.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using GPTServer.Common.Utils.GPTClient.Clients;

namespace GPTServer.Common.Utils.GPTClient.WireUp;

public static class GPTClientServiceCollectionExtensions
{
    public static void AddGPTClient(this IServiceCollection services, string redisUrl = "")
    {
        services.AddScoped<IGPTClient, DefaultGPTClient>();
    }
}
