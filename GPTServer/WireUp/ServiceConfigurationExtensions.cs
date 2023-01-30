using AspNetCoreRateLimit;
using GPTServer.Configurations;
using GPTServer.Constants;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using Serilog;

namespace GPTServer.WireUp;

public static class ServiceConfigurationExtensions
{
    public static void AddCustomSerilog(this IConfiguration config, IWebHostEnvironment env)
    {
        var logOptions = config.GetSection(nameof(LogOptions));
        string customLogContainerPath = logOptions.Get<LogOptions>().LogPath;

        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.Async(asyncConfig =>
            {
                string envName = EnvironmentConstants.DEV_FOR_LOGGING;

                if (env.EnvironmentName.Equals(EnvironmentConstants.PRODUCTION))
                {
                    envName = EnvironmentConstants.PROD_FOR_LOGGING;
                }
                else if (env.EnvironmentName.Equals(EnvironmentConstants.TEST))
                {
                    envName = EnvironmentConstants.TEST_FOR_LOGGING;
                }

                string filePath = Path.Combine(customLogContainerPath, "Log-{Date}.log");
                asyncConfig.File(
                        $"{customLogContainerPath}\\Log-{envName.ToUpper()}-",
                        rollingInterval: RollingInterval.Day,
                        outputTemplate: "[{Timestamp:yyyy.MM.dd-HH:mm:ss.fff}][{Level:u3}][{partnersz}|{partnerNev}|{guestToken}|{requestId}]: {Message:lj}{NewLine}{Exception}");
            })
            .CreateLogger();
    }

    /// <summary>
    /// Registers AspNetCoreRateLimiting for rate limiting solution.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddRateLimiting(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddOptions();

        services.AddMemoryCache();

        services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
        services.Configure<IpRateLimitPolicies>(Configuration.GetSection("IpRateLimitPolicies"));

        services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
    }

    public static void UseCustomExceptionHandling(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(a => a.Run(async context =>
        {
            await Task.FromResult(() =>
            {
                var feature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = feature?.Error;

                string result = JsonConvert.SerializeObject(new { error = exception?.Message });
                context.Response.ContentType = "application/json";
            });
        }));
    }

    /// <summary>
    /// Registers the given T type as configuration for scoped and singleton usage.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddConfiguration<T>(this IServiceCollection services, IConfiguration configuration)
        where T : class, new()
    {
        T options = new();

        configuration.GetSection(typeof(T).Name).Bind(options);

        var section = configuration.GetSection(typeof(T).Name);

        services.Configure<T>(section);
        services.AddSingleton(options);
    }

}
