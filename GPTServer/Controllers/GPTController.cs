using GPTServer.Api.GPT;
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
    public async Task TestAsync() =>
        await _gptService.TestAsync();
}
