using GPTServer.Api.GPT;

namespace GPTServer.Services.GPT;

public class GPTService : IGPTService
{
    private readonly HttpClient _httpClient;

    public GPTService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task TestAsync()
    {
        throw new NotImplementedException();
    }
}
