using GPTServer.Common.DataAccess.Cache.Interfaces;
using GPTServer.Common.DataAccess.Cache.Lazy;
using GPTServer.Common.DataAccess.Cache.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace GPTServer.Common.DataAccess.WireUp
{
    public static class ServiceCollectionExtensions
    {

        public static void AddDataAccess(this IServiceCollection services, string redisUrl = "")
        {
            if (!string.IsNullOrWhiteSpace(redisUrl))
            {
                services.AddSingleton(typeof(ICache<>), typeof(RedisCache<>));
                services.AddStackExchangeRedisCache(options => options.Configuration = redisUrl);
            }
            else
            {
                services.AddSingleton(typeof(ICache<>), typeof(LazyCache<>));
                services.AddLazyCache();
            }
        }
    }
}
