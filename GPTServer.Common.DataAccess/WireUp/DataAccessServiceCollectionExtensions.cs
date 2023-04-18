using GPTServer.Common.Core.Configurations;
using GPTServer.Common.DataAccess.Cache.Interfaces;
using GPTServer.Common.DataAccess.Cache.Lazy;
using GPTServer.Common.DataAccess.Cache.Redis;
using GPTServer.Common.DataAccess.DbContexts;
using GPTServer.Common.DataAccess.Repositories;
using GPTServer.Common.DataAccess.Repositories.Interfaces;
using GPTServer.Common.DataAccess.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GPTServer.Common.DataAccess.WireUp
{
    public static class DataAccessServiceCollectionExtensions
    {
        public static void AddDataAccess(
            this IServiceCollection services,
            IConfiguration configuration,
            string redisUrl = "")
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

            var dbSection = configuration.GetSection(nameof(DbOptions));
            var dbOptions = dbSection.Get<DbOptions>();

            services.AddDbContext<GPTDbContext>(
                options => options
                    .UseSqlServer(
                        dbOptions?.ConnectionString,
                        optionsBuilder => optionsBuilder.MigrationsAssembly(dbOptions?.MigrationAssembly)),
                ServiceLifetime.Scoped);


            services.AddScoped<IDatabaseTransaction, DatabaseTransaction>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IApiKeyRepo, ApiKeyRepo>();
        }
    }
}
