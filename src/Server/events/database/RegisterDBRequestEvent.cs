namespace MMOLibrary.Server;
using Riptide;

public class RegisterDBRequestEvent : EventArgs
{
    public Connection Authenticate { get; }
    public ushort GatewayId { get; }
    public ushort ClientId { get; }
    public string Username { get; }
    public string Password { get; }

    public RegisterDBRequestEvent(Connection authenticate, ushort gatewayId, ushort clientId, string username, string password)
    {
        Authenticate = authenticate;
        GatewayId = gatewayId;
        ClientId = clientId;
        Username = username;
        Password = password;
    }
}