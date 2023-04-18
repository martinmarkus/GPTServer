using GPTServer.Api.Clients;
using OpenAI_API;
using OpenAI_API.Completions;

namespace GPTServer.Clients.GPT;

public class GPTClient : IGPTClient
{

    public GPTClient()
    {

    }

    public async Task CreateCompletionAsync(string apiKey, string organization)
    {
        var api = new OpenAIAPI(new APIAuthentication(apiKey ?? string.Empty, organization ?? string.Empty));
        
        var result = await api.Completions.CreateCompletionAsync(
            new CompletionRequest(
                model: OpenAI_API.Models.Model.GPT4,
                prompt: "One Two Three One Two"
            )); 
    }
}
