namespace ValorRiseServer.Messages;

using ValorRiseClient;
using Riptide;

internal class LoginAuthRequest : IMessageHandler
{
    private readonly EventBus _eventBus;

    public LoginAuthRequest(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(ushort gatewayId, Message message)
    {
        ushort clientId = message.GetUShort();
        string username = message.GetString();
        string password = message.GetString();

        if (!MMOServer.TryGetClient(gatewayId, out var gateway)) throw new InvalidOperationException("Gateway not found for specified clientId");
        var args = new LoginAuthRequestEvent(gateway, clientId, username, password);
        _eventBus.Publish(args);
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
