using Riptide;

namespace MMOLibrary.Server;

public class LoginRequestEvent : EventArgs
{
  public Connection Client { get; }
  public string Username { get; }
  public string Password { get; }

  public LoginRequestEvent(Connection client, string username, string password)
  {
    Client = client;
    Username = username;
    Password = password;
  }
}
