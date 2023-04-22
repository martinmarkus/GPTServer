namespace GPTServer.Web.Extensions;

public static class HttpContextExtensions
{
	public static string GetFirstHeaderValueOrDefault(this HttpContext context, string headerKey, string defaultValue)
	{
		string headerVlaue = context.Request
			 ?.Headers[headerKey]
			 .FirstOrDefault()
			 ?? string.Empty;

		return !string.IsNullOrEmpty(headerVlaue) ? headerVlaue : defaultValue;
	}
}
