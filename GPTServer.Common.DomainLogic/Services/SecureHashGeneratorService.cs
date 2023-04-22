using System.Security.Cryptography;
using System.Text;

namespace GPTServer.Common.DomainLogic.Interfaces;

public class SecureHashGeneratorService : ISecureHashGeneratorService
{
    private const int SALT_SIZE = 128;
    private const int HASH_SIZE = 128;
    private const int ITERATIONS = 100000;

    public string CreateHMACSHA256(string value, string key)
    {
        HMACSHA256 hasher = new(Encoding.ASCII.GetBytes(key));
        byte[] hash = hasher.ComputeHash(Encoding.ASCII.GetBytes(value));

        return BitConverter.ToString(hash)
            .Replace("-", "")
            .ToLower();
    }

    public string CreateHash(string value, string salt, int hashSize = HASH_SIZE)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(salt))
        {
            throw new ArgumentException();
        }

        string resultHash = string.Empty;

        try
        {
            byte[] saltArray = Convert.FromBase64String(salt.ToLower());
            Rfc2898DeriveBytes pbkdf2 = new(value, saltArray, ITERATIONS);

            byte[] hashBytes = pbkdf2.GetBytes(hashSize);
            resultHash = Convert.ToBase64String(hashBytes);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        return resultHash?.ToUpper() ?? string.Empty;
    }

    public string CreateSalt()
    {
        string resultSalt = string.Empty;

        try
        {
            RandomNumberGenerator provider = RandomNumberGenerator.Create();
            byte[] rawSalt = new byte[SALT_SIZE];
            provider.GetBytes(rawSalt);

            resultSalt = Convert.ToBase64String(rawSalt)[..SALT_SIZE];
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        return resultSalt?.ToUpper() ?? string.Empty;
    }
}
