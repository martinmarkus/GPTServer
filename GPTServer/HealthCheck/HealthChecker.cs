using GPTServer.Common.Core.Configurations;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace GPTServer.Web.HealthCheck;

public class HealthChecker : IHealthCheck
{
    private readonly string _appName;

    public HealthChecker(IOptions<BaseOptions> options)
    {
        _appName = options.Value.AppName;
    }

    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            return Task.FromResult(HealthCheckResult.Healthy(_appName));
        }
        catch (Exception e)
        {
            return Task.FromResult(HealthCheckResult.Unhealthy(_appName, e));
        }
    }
}