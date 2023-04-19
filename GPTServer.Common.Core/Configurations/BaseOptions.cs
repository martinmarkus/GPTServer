namespace GPTServer.Common.Core.Configurations;

public class BaseOptions
{
    public string AppName { get; set; }
    public string EnvironmentName { get; set; }
    public string ApiKey { get; set; }
    public string AuthIssuer { get; set; }
    public string AuthSecretKey { get; set; }
    public int AuthExpirationMinutes { get; set; }
}
