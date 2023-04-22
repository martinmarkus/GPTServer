using GPTServer.Common.Core.DTOs.General;
using GPTServer.Common.Core.DTOs.GPT;
using GPTServer.Common.Core.GPT.DTOs;

namespace GPTServer.Common.DomainLogic.Interfaces;

public interface IGPTService
{
    Task<GPTAnswerResponseDTO> GetGPTAnswerAsync(GPTQuestionRequestDTO @params);
    Task<ApiKeysResponseDTO> GetOwnApiKeysAsync();
    Task<ApiKeysResponseDTO> AddActiveApiKeyAsync(ApiKeyRequestDTO dto);
    Task<ApiKeysResponseDTO> RemoveApiKeyAsync(ApiKeyRequestDTO dto);
    Task<ApiKeysResponseDTO> SetActiveApiKeyAsync(ApiKeyRequestDTO dto);
}
