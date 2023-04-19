using GPTServer.Common.Core.Enums;

namespace GPTServer.Common.Core.DTOs.General;
public class BaseResponseDTO
{
    public ResponseType ResponseType { get; set; } = ResponseType.Success;

    public string ErrorMessage { get; set; } = string.Empty;
}
