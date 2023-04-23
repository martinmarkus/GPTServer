using GPTServer.Common.Core.DTOs.Authentication;
using GPTServer.Common.Core.DTOs.General;

namespace GPTServer.Common.DomainLogic.Interfaces;

public interface IAuthService
{
    Task<BaseResponseDTO> ConfirmCustomerAsync(ConfirmCustomerRequestDTO dto);
    Task<UserResponseDTO> GetUserAsync();
    Task<LoginResponseDTO> LoginAsync(LoginRequestDTO dto);
    Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO dto);
}
