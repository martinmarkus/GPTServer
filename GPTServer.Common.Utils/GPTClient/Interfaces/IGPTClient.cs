using GPTServer.Common.Utils.GPTClient.DataObjects;
using OpenAI.Completions;

namespace GPTServer.Api.Clients;

public interface IGPTClient
{
    Task<GPTCompletionResponse> CreateCompletionAsync(GPTCompletionRequest request);
}
