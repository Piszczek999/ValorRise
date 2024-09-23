namespace MMO_Library.Server;

public class RegisterRequestEvent : EventArgs
{
  public Connection Connection { get; }
  public string Username { get; }
  public string Password { get; }

  public RegisterRequestEvent(Connection connection, string username, string password)
  {
    Connection = connection;
    Username = username;
    Password = password;
  }

  public RegisterResult Validate()
  {
    if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
    {
      return RegisterResult.InvalidCredentials;
    }
    if (Username.Length < 6 || Password.Length < 8)
    {
      return RegisterResult.InvalidCredentials;
    }
    if (!Username.Any(char.IsLetterOrDigit))
    {
      return RegisterResult.InvalidCredentials;
    }
    return RegisterResult.Success;
  }
}