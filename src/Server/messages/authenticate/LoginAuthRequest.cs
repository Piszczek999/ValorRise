using Riptide;

namespace ValorRise.Server.Messages;

[Message((ushort)MessageType.ToAuthenticate.LoginAuthRequest)]
internal class LoginAuthRequest : IMessageHandler
{
    public void HandleMessage(ushort gatewayId, Message message)
    {
        ushort clientId = message.GetUShort();
        string username = message.GetString();
        string password = message.GetString();

        if (!MMOServer.TryGetClient(gatewayId, out var gateway)) throw new InvalidOperationException("Gateway not found for specified clientId");
        var args = new LoginAuthRequestEvent(gateway, clientId, username, password);
        MMOServer.EventBus.Publish(args);
    }
}

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
