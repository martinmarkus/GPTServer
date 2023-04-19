using GPTServer.Api.Clients;
using GPTServer.Common.Utils.GPTClient.DataObjects;
using OpenAI_API;
using OpenAI_API.Completions;
using Org.BouncyCastle.Asn1.Crmf;

namespace GPTServer.Clients.GPT;

public class GPTClient : IGPTClient
{

    public GPTClient()
    {

    }

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
