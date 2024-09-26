using Riptide;

namespace MMOLibrary.Server;

public class RegisterAuthRequestEvent : EventArgs
{
    public Connection Gateway { get; }
    public ushort ClientId { get; }
    public string Username { get; }
    public string Password { get; }

    public RegisterAuthRequestEvent(Connection gateway, ushort clientId, string username, string password)
    {
        Gateway = gateway;
        ClientId = clientId;
        Username = username;
        Password = password;
    }
}