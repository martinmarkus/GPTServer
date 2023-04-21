using AspNetCoreRateLimit;
using GPTServer.Common.Core.Configurations;
using GPTServer.Common.Core.Constants;
using GPTServer.Common.Core.ContextInfo;
using GPTServer.Common.DataAccess.DbContexts;
using GPTServer.Common.DataAccess.WireUp;
using GPTServer.Common.DomainLogic.WireUp;
using GPTServer.Web.Extendions;
using GPTServer.Web.HealthCheck;
using GPTServer.Web.Middlewares;
using GPTServer.Web.Request;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IO.Compression;
using System.Text;

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
		var baseSection = Configuration.GetSection(nameof(BaseOptions));
		var baseOptions = baseSection.Get<BaseOptions>();

        if (baseOptions is null
            || string.IsNullOrEmpty(baseOptions.AppName)
            || string.IsNullOrEmpty(baseOptions.AuthIssuer)
            || string.IsNullOrEmpty(baseOptions.AuthSecretKey))
        {
            throw new Exception();
        }

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

		services.AddAuthentication(oOptions =>
			{
				oOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				oOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

				// INFO: Default auth with UserAuthHandler
				oOptions.DefaultAuthenticateScheme = AuthSchemeConstants.UserAuthScheme;
			})
			.AddJwtBearer(oOptions =>
			{
				oOptions.SaveToken = true;
				oOptions.TokenValidationParameters = new TokenValidationParameters
				{
					RequireExpirationTime = false,
					ValidateLifetime = false,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(baseOptions.AuthSecretKey)),
					ValidateIssuer = true,
					ValidIssuer = baseOptions.AuthIssuer,
					ValidateAudience = false
				};
			})
			.AddScheme<UserAuthSchemeOptions, UserAuthHandler>(AuthSchemeConstants.UserAuthScheme, options => { });

		services.AddDataAccess(Configuration);
        services.AddDomainLogic();
    }

    public virtual void Configure(
        IApplicationBuilder app,
        IWebHostEnvironment env,
        IServiceScopeFactory serviceScopeFactory,
        GPTDbContext dbContext)
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

        dbContext.Database.Migrate();
    }

    protected virtual bool IsDevelopmentEnvironment() =>
        Environment.EnvironmentName.Equals(
            EnvironmentConstants.DEVELOPMENT,
            StringComparison.OrdinalIgnoreCase);
}
