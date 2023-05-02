namespace GPTServer.Common.Core.DTOs.GPT;
public class ApiKeyResponseDTO
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public string KeyName { get; set; }
    public string Key { get; set; }
    public DateTime CreationDate { get; set; }
}
