using System.Security.Cryptography;

namespace ValorRiseAuthenticate;

public class TokenGenerator
{
    public static string GenerateToken()
    {
        int randomNumber = new Random().Next(int.MinValue, int.MaxValue);
        byte[] bytes = BitConverter.GetBytes(randomNumber);

        SHA256 sha256Hash = SHA256.Create();
        byte[] hashBytes = sha256Hash.ComputeHash(bytes);
        string hashString = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

        long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        return hashString + timestamp;
    }
}