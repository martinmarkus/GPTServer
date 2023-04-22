using GPTServer.Common.Core.DTOs.Authentication;
using GPTServer.Common.Core.DTOs.General;
using GPTServer.Common.DomainLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GPTServer.Web.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthenticationController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO dto) =>
        await _authService.LoginAsync(dto);

    [HttpPost]
    public async Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO dto) =>
        await _authService.RegisterAsync(dto);

    [HttpPost]
    public async Task<BaseResponseDTO> ConfirmCustomerAsync(ConfirmCustomerRequestDTO dto) =>
        await _authService.ConfirmCustomerAsync(dto);
}
