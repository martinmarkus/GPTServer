using GPTServer.Common.Core.DTOs.GPT;
using GPTServer.Common.Core.GPT.DTOs;

namespace GPTServer.Common.DomainLogic.Interfaces;

public interface IGPTService
{
    Task<GPTAnswerResponseDTO> GetGPTAnswerAsync(GPTQuestionRequestDTO @params);
}
