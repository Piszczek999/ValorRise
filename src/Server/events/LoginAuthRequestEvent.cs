using Riptide;

namespace MMO_Library.Server;

public class LoginAuthRequestEvent : EventArgs
{
    public Connection Gateway { get; }
    public ushort ClientId { get; }
    public string Username { get; }
    public string Password { get; }

    public LoginAuthRequestEvent(Connection gateway, ushort clientId, string username, string password)
    {
        Gateway = gateway;
        ClientId = clientId;
        Username = username;
        Password = password;
    }
}
