using GPTServer.Common.Core.DTOs.GPT;
using GPTServer.Common.Core.GPT.DTOs;
using GPTServer.Common.DomainLogic.Interfaces;

namespace GPTServer.Common.DomainLogic.Services;

public class GPTService : IGPTService
{
    public Task<GPTAnswerResponseDTO> GetGPTAnswerAsync(GPTQuestionRequestDTO @params)
    {
        throw new NotImplementedException();
    }
}
