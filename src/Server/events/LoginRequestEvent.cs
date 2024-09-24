using Riptide;

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
}
