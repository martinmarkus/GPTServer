using GPTServer.Common.Core.DTOs.GPT;
using GPTServer.Common.DomainLogic.Interfaces;
using GPTServer.Web.Request.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GPTServer.Web.Controllers;

[ServiceFilter(typeof(ExtensionPermission))]
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
    public async Task<ApiKeysResponseDTO> SetActiveApiKeyAsync(ApiKeyRequestDTO dto) =>
        await _gptService.SetActiveApiKeyAsync(dto);

    [HttpPost]
    [Authorize]
    public async Task<ApiKeysResponseDTO> AddOrUpdateActiveApiKeyAsync(ApiKeyRequestDTO dto) =>
    await _gptService.AddOrUpdateActiveApiKeyAsync(dto);

    [HttpPost]
    [Authorize]
    public async Task<ApiKeysResponseDTO> RemoveApiKeyAsync(ApiKeyRequestDTO dto) =>
        await _gptService.RemoveApiKeyAsync(dto);
}
