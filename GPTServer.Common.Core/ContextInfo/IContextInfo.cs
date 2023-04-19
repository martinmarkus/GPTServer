namespace GPTServer.Common.Core.ContextInfo;

public interface IContextInfo
{
    public Guid? UserId { get; set; }
    public string Email { get; set; }
    public string AuthToken { get; set; }
    public string ClientIP { get; set; }
    public string UserAgent { get; set; }
    public string Language { get; set; }
}
