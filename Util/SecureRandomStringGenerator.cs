using System.Security.Cryptography;
using System.Text;

namespace CrudAPI.Etc;

public class SecureRandomStringGenerator
{
    private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+{}[]<>|";

    public static string Generate(int length)
    {
        var stringBuilder = new StringBuilder(length);
        using (var rng =RandomNumberGenerator.Create())
        {
            byte[] randomBytes = new byte[length];
            rng.GetBytes(randomBytes);

            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(chars[randomBytes[i] % chars.Length]);
            }
        }
        return stringBuilder.ToString();
    }
}