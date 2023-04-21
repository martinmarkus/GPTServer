using GPTServer.Common.Core.ContextInfo;

namespace GPTServer.Web.Request;

public class ContextInfo : IContextInfo
{
	public Guid? UserId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	public string Email { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	public string AuthToken { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	public string ClientIP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	public string UserAgent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	public string Language { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
