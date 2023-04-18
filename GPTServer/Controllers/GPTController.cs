using GPTServer.Common.Core.DTOs.GPT;
using GPTServer.Common.Core.GPT.DTOs;
using GPTServer.Common.DomainLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GPTServer.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class GPTController : ControllerBase
{
    private readonly IGPTService _gptService;

    public GPTController(IGPTService gptService)
    {
        _gptService = gptService;
    }

    [HttpPost]
    public async Task<GPTAnswerResponseDTO> GetGPTAnswerAsync(GPTQuestionRequestDTO @params) =>
        await _gptService.GetGPTAnswerAsync(@params);
}
