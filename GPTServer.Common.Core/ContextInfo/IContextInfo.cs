namespace GPTServer.Common.Core.ContextInfo;

public interface IContextInfo
{
    public Guid? UserId { get; }
    public string Email { get; }
    public string AuthToken { get; }
    public string ClientIP { get; }
    public string UserAgent { get; }
    public string Language { get; }

    void SetValues(Guid? userId, string email, string authToken, string authRoutingEnvironment, string clientIP, string userAgent, string language);
}
