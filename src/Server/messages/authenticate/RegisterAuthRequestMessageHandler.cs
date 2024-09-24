namespace MMO_Library.Server;
using Riptide;

internal class RegisterAuthRequestMessageHandler : IMessageHandler
{
    private readonly EventBus _eventBus;
    private readonly Server _server;

    public RegisterAuthRequestMessageHandler(EventBus eventBus, Server server)
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
        var args = new RegisterAuthRequestEvent(gateway, clientId, username, password);
        _eventBus.Publish(args);
    }
}