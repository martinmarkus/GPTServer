using GPTServer.Common.Core.DTOs.General;

namespace GPTServer.Common.Core.DTOs.GPT;

public class ApiKeysResponseDTO : BaseResponseDTO
{
    public IReadOnlyList<ApiKeyResponseDTO> Keys { get; set; }
}
