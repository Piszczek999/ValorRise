namespace MMOLibrary.Server;
using MongoDB.Bson;
using Riptide;

internal class LoginAuthRequestMessageHandler : IMessageHandler
{
    private readonly EventBus _eventBus;
    private readonly MMOServer _server;

    public LoginAuthRequestMessageHandler(EventBus eventBus, MMOServer server)
    {
        _eventBus = eventBus;
        _server = server;
    }

    public void HandleMessage(ushort gatewayId, Message message)
    {
        ushort clientId = message.GetUShort();
        string username = message.GetString();
        string password = message.GetString();

        if (!_server.TryGetClient(gatewayId, out var gateway)) throw new InvalidOperationException("Gateway not found for specified clientId");
        var args = new LoginAuthRequestEvent(gateway, clientId, username, password);
        _eventBus.Publish(args);
    }
}