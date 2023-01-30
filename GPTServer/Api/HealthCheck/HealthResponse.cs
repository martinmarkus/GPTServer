namespace GPTServer.Api.HealthCheck;

public class HealthResponse
{
    public string Status { get; set; }

    public string AppName { get; set; }

    public double DurationMs { get; set; }

    public string Error { get; set; }
}