namespace GPTServer.Common.Core.DTOs.GPT;
public class ApiKeyRequestDTO
{
    public Guid Id { get; set; }
    public string ApiKey { get; set; }
    public string ApiKeyName { get; set; }
}
