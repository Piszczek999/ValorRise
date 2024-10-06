using Riptide;

namespace ValorRise.Server.Messages;

[Message((ushort)MessageType.ToAuthenticate.RegisterAuthRequest)]
internal class RegisterAuthRequest : IMessageHandler
{
    private GlobalEventHandler _eventHandler = MMOServer.GlobalEventHandler;

    public void HandleMessage(ushort gatewayId, Message message)
    {
        ushort clientId = message.GetUShort();
        string username = message.GetString();
        string password = message.GetString();

        var args = new RegisterAuthRequestEvent(gatewayId, clientId, username, password);
        _eventHandler.InvokeEvent(args);
    }
}

public class RegisterAuthRequestEvent : EventArgs
{
    public ushort GatewayId { get; }
    public Connection Gateway { get => MMOServer.TryGetClient(GatewayId, out var gateway) ? gateway : null; }
    public ushort ClientId { get; }
    public string Username { get; }
    public string Password { get; }

    public RegisterAuthRequestEvent(ushort gatewayId, ushort clientId, string username, string password)
    {
        GatewayId = gatewayId;
        ClientId = clientId;
        Username = username;
        Password = password;
    }
}