namespace GPTServer.Api.Clients;

public interface IGPTClient
{
    Task CreateCompletionAsync(string apiKey, string organization);
}
