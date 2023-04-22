namespace GPTServer.Common.Core.DTOs.GPT;
public class ApiKeyResponseDTO
{
    public bool IsActive { get; set; }
    public string KeyName { get; set; }
    public string Key { get; set; }
}
