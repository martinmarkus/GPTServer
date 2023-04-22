using GPTServer.Common.Utils.GPTClient.DataObjects;

namespace GPTServer.Common.Utils.GPTClient.Interfaces;

public interface IGPTClient
{
    Task<GPTCompletionResponse> CreateCompletionAsync(GPTCompletionRequest request);
}
