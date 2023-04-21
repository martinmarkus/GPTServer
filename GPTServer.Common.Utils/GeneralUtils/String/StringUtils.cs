using System.Linq;

namespace GPTServer.Common.Core.Utils.GeneralUtils.String;

public static class StringUtils
{
    public static string SafeSubstring(this string value, int startIndex, int length)
    {
        return new string((value ?? string.Empty)
            .Skip(startIndex)
            .Take(length)
            .ToArray());
    }
}
