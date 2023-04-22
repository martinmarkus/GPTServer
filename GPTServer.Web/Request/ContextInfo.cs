using GPTServer.Common.Core.ContextInfo;

namespace GPTServer.Web.Request;

public class ContextInfo : IContextInfo
{
    public Guid? UserId { get; private set; }
    public string Email { get; private set; }
    public string AuthToken { get; private set; }
    public string AuthRoutingEnvironment { get; private set; }
    public string ClientIP { get; private set; }
    public string UserAgent { get; private set; }
    public string Language { get; private set; }

    private bool _valuesSet = false;

    public void SetValues(
        Guid? userId,
        string email,
        string authToken,
        string authRoutingEnvironment,
        string clientIP,
        string userAgent,
        string language)
    {
        if (_valuesSet)
        {
            throw new InvalidOperationException("The actual ContextInfo is already set.");
        }

        UserId = userId;
        Email = email;
        AuthToken = authToken;
        AuthRoutingEnvironment = authRoutingEnvironment;
        ClientIP = clientIP;
        UserAgent = userAgent;
        Language = language;

        _valuesSet = true;
    }
}
