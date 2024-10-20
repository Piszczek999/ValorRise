namespace ValorRiseAuthenticate;

public static class BCryptPasswordHasher
{
    public static bool Verify(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }

    public static string HashPassword(string password, string salt)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, salt);
    }

    public static string GenerateSalt()
    {
        return BCrypt.Net.BCrypt.GenerateSalt();
    }
}