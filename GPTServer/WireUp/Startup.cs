using AspNetCoreRateLimit;
using GPTServer.Common.Core.Configurations;
using GPTServer.Common.Core.Constants;
using GPTServer.Common.Core.ContextInfo;
using GPTServer.Common.DataAccess.WireUp;
using GPTServer.Common.DomainLogic.WireUp;
using GPTServer.Web.Extendions;
using GPTServer.Web.HealthCheck;
using GPTServer.Web.Middlewares;
using GPTServer.Web.Request;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Serilog;
using System.IO.Compression;

namespace GPTServer.Web.WireUp;

public class Startup
{
    private IConfiguration Configuration { get; }
    private IWebHostEnvironment Environment { get; }

    public Startup(
        IConfiguration configuration,
        IWebHostEnvironment env)
    {
        Configuration = configuration;
        Environment = env;
    }

    public virtual void ConfigureServices(IServiceCollection services)
    {
        // INFO: Register configurations
        services.AddConfiguration<BaseOptions>(Configuration);
        services.AddConfiguration<CachingOptions>(Configuration);
        services.AddConfiguration<DbOptions>(Configuration);
        services.AddConfiguration<LogOptions>(Configuration);
        services.AddConfiguration<SmtpOptions>(Configuration);

        // INFO: Cookie policies
        services.Configure<CookiePolicyOptions>(options =>
        {
            options.CheckConsentNeeded = context => false;
            options.Secure = CookieSecurePolicy.None;
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });

        // INFO: Default healthcheck
        services.AddHealthChecks().AddCheck<HealthChecker>("Health Check");

        // INFO: Add Serilog logging
        Configuration.AddCustomSerilog(Environment);

        // INFO: Allow sync calls for IIS and Kestrel
        services.Configure<IISServerOptions>(options =>
        {
            options.AllowSynchronousIO = true;
        });

        services.Configure<KestrelServerOptions>(options =>
        {
            options.AllowSynchronousIO = true;
        });

        services.AddRateLimiting(Configuration);
        services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

        services.AddControllers();
        services.AddHttpClient();

        services.Configure<BrotliCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
        services.AddResponseCompression(options =>
        {
            options.Providers.Add<BrotliCompressionProvider>();
        });

        services.AddScoped<IContextInfo, ContextInfo>();

        services.AddDataAccess(Configuration);
        services.AddDomainLogic();
    }

    public virtual void Configure(
        IApplicationBuilder app,
        IWebHostEnvironment env,
        IServiceScopeFactory serviceScopeFactory)
    {
        app.UseIpRateLimiting();
        app.UseSerilogRequestLogging();

        // INFO: Middlewares
        app.UseMiddleware<ContextInfoMiddleware>();

        app.UseRouting();
        app.UseCookiePolicy();
        app.UseResponseCompression();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapCustomHealthChecks();
        });

        bool isDevEnv = IsDevelopmentEnvironment();

        // INFO: For nginx hosting
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });

        app.UseCustomExceptionHandling();

        // INFO: Must be placed after Cors
        app.UseHttpsRedirection();

        if (!isDevEnv)
        {
            app.UseHsts();
        }

        if (isDevEnv)
        {
            app.UseDeveloperExceptionPage();
        }
    }

    protected virtual bool IsDevelopmentEnvironment() =>
        Environment.EnvironmentName.Equals(
            EnvironmentConstants.DEVELOPMENT,
            StringComparison.OrdinalIgnoreCase);
}
