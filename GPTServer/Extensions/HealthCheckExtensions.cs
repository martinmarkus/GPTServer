using GPTServer.Common.Core.DTOs.HealthCheck;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System.Net.Mime;

namespace GPTServer.Web.Extendions;

public static class HealthCheckExtensions
{
    public static IEndpointConventionBuilder MapCustomHealthChecks(
        this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapHealthChecks("/ping", new HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                var response = report.Entries
                    .Select(e => new HealthResponse
                    {
                        Status = report.Status.ToString(),
                        AppName = e.Value.Description ?? "GPTServer Application",
                        DurationMs = report.TotalDuration.TotalMilliseconds,
                        Error = e.Value.Exception?.Message ?? string.Empty
                    })
                    .FirstOrDefault();

                string result = JsonConvert.SerializeObject(
                    response,
                    Formatting.None,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });

                context.Response.ContentType = MediaTypeNames.Application.Json;
                await context.Response.WriteAsync(result);
            }
        });
    }
}