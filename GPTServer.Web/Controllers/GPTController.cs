using GPTServer.Common.Core.DTOs.GPT;
using GPTServer.Common.DomainLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GPTServer.Web.Controllers;

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
    [Authorize]
    public async Task<ApiKeysResponseDTO> GetOwnApiKeysAsync() =>
        await _gptService.GetOwnApiKeysAsync();

    [HttpPost]
    [Authorize]
    public async Task SetActiveApiKeyAsync(ApiKeyRequestDTO dto) =>
        await _gptService.SetActiveApiKeyAsync(dto);

    [HttpPost]
    [Authorize]
    public async Task AddApiKeyAsync(ApiKeyRequestDTO dto) =>
    await _gptService.AddActiveApiKeyAsync(dto);

    [HttpPost]
    [Authorize]
    public async Task RemoveApiKeyAsync(ApiKeyRequestDTO dto) =>
        await _gptService.RemoveApiKeyAsync(dto);
}
