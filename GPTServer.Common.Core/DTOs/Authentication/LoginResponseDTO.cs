using GPTServer.Common.Core.DTOs.General;

namespace GPTServer.Common.Core.DTOs.Authentication;
public class LoginResponseDTO : BaseResponseDTO
{
    public string AuthToken { get; set; }
}
