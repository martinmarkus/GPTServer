using GPTServer.Common.Utils.GPTClient.DataObjects;
using GPTServer.Common.Utils.GPTClient.Interfaces;
using OpenAI_API;
using OpenAI_API.Completions;

namespace GPTServer.Common.Utils.GPTClient.Clients;

public class DefaultGPTClient : IGPTClient
{
    public async Task<GPTCompletionResponse> CreateCompletionAsync(GPTCompletionRequest request)
    {
        if (request is null)
        {
            return new();
        }

        var api = new OpenAIAPI(new APIAuthentication(request.ApiKey ?? string.Empty, openAIOrganization: string.Empty));

        var result = await api.Completions.CreateCompletionAsync(
            new CompletionRequest(
                model: OpenAI_API.Models.Model.GPT4,
                prompt: "One Two Three One Two"
            ));

        return new();
    }
}
