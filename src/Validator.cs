namespace ValorRiseClient;

public static class Validator
{
    public static bool AreCredentialsValid(string username, string password)
    {
        return ValidateCredentials(username, password);
    }

    public static bool AreCredentialsValid(string username, string password, string passwordRepeat)
    {
        return password == passwordRepeat && ValidateCredentials(username, password);
    }

    private static bool ValidateCredentials(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            return false;
        }
        if (username.Length < 6 || password.Length < 8)
        {
            return false;
        }
        if (!username.Any(char.IsLetterOrDigit))
        {
            return false;
        }
        return true;
    }
}