using System.Security.Cryptography;

namespace GPTServer.Common.Core.Utils.GeneralUtils.String;

public static class StringGenerator
{
    private static readonly string AlphabeticChars = "ABCDEFGHJKLMNOPRSTUVWXYZ";
    private static readonly string NumericChars = "23456789";

    public static string GetRandomStringBasedOnGuid(int length = 10)
    {
        if (length < 0)
        {
            length = 1;
        }

        if (length > 32)
        {
            length = 32;
        }

        return Guid.NewGuid()
            .ToString("N")[..length]
            .ToUpper();
    }

    public static string GetRandomAlphabeticString(int length = 8) =>
        GetRandomString(length, AlphabeticChars);

    public static string GetRandomNumericString(int length = 8) =>
        GetRandomString(length, NumericChars);

    public static string GetRandomAlphanumericString(int length = 8) =>
        GetRandomString(length, $"{AlphabeticChars}{NumericChars}");

    public static string GetRandomString(int length = 8, string chars = "")
    {
        string randomString = Convert.ToBase64String(RandomNumberGenerator.GetBytes(length * 10)).ToUpper();

        if (string.IsNullOrEmpty(chars))
        {
            return randomString[..length];
        }

        List<char> matchingStringList = randomString
            .Where(x => chars.Contains(x))
            .ToList();

        string matchingString = string.Join(string.Empty, matchingStringList)[..length];

        return matchingString;
    }
}
