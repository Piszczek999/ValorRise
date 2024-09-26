namespace MMOLibrary.Server;
using MongoDB.Bson;
using Riptide;

internal class NewCharacterAuthRequestMessageHandler : IMessageHandler
{
    private readonly EventBus _eventBus;
    private readonly MMOServer _server;

    public NewCharacterAuthRequestMessageHandler(EventBus eventBus, MMOServer server)
    {
        _eventBus = eventBus;
        _server = server;
    }

    public void HandleMessage(ushort gatewayId, Message message)
    {
        ushort clientId = message.GetUShort();
        ObjectId userId = message.GetObjectId();
        string name = message.GetString();

        if (!_server.TryGetClient(gatewayId, out var gateway)) throw new InvalidOperationException("Gateway not found for specified clientId");
        var args = new NewCharacterAuthRequestEvent(gateway, clientId, userId, name);
        _eventBus.Publish(args);
    }
}