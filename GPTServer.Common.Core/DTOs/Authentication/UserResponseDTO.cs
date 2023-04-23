using GPTServer.Common.Core.DTOs.General;
using GPTServer.Common.Core.DTOs.GPT;

namespace GPTServer.Common.Core.DTOs.Authentication;

public class UserResponseDTO : BaseResponseDTO
{
    public string Email { get; set; }

    public ApiKeysResponseDTO ApiKeysResponseDTO { get; set; }
}
