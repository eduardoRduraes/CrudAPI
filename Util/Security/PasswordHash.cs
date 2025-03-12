using BCrypt.Net;

namespace CrudAPI.Util;

public class PasswordHash
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string providedPassword)
    {
        if(string.IsNullOrEmpty(password) || string.IsNullOrEmpty(providedPassword))
            return false;
        
        try
        {
            return BCrypt.Net.BCrypt.Verify(password, providedPassword);
        }
        catch (SaltParseException e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}