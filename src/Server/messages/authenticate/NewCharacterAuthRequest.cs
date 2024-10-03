namespace MMOLibrary.Server.Messages;
using MongoDB.Bson;
using Riptide;

internal class NewCharacterAuthRequest : IMessageHandler
{
    private readonly EventBus _eventBus;

    public NewCharacterAuthRequest(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(ushort gatewayId, Message message)
    {
        ushort clientId = message.GetUShort();
        ObjectId userId = message.GetObjectId();
        string name = message.GetString();

        if (!MMOServer.TryGetClient(gatewayId, out var gateway)) throw new InvalidOperationException("Gateway not found for specified clientId");
        var args = new NewCharacterAuthRequestEvent(gateway, clientId, userId, name);
        _eventBus.Publish(args);
    }
}