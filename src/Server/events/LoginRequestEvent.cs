namespace MMO_Library.Server;

public class LoginRequestEvent : EventArgs
{
  public Connection Connection { get; }
  public string Username { get; }
  public string Password { get; }

  public LoginRequestEvent(Connection connection, string username, string password)
  {
    Connection = connection;
    Username = username;
    Password = password;
  }

  public LoginResult Validate()
  {
    if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
    {
      return LoginResult.IncorrectCredentials;
    }
    if (Username.Length < 6 || Password.Length < 8)
    {
      return LoginResult.IncorrectCredentials;
    }
    if (!Username.Any(char.IsLetterOrDigit))
    {
      return LoginResult.IncorrectCredentials;
    }
    return LoginResult.Success;
  }
}
