using Riptide;

namespace MMOLibrary.Server;

public class RegisterRequestEvent : EventArgs
{
  public Connection Client { get; }
  public string Username { get; }
  public string Password { get; }

  public RegisterRequestEvent(Connection client, string username, string password)
  {
    Client = client;
    Username = username;
    Password = password;
  }
}