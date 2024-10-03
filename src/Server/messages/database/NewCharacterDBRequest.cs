namespace MMOLibrary.Server.Messages;

using MongoDB.Bson;
using Riptide;

internal class NewCharacterDBRequest : IMessageHandler
{
    private readonly EventBus _eventBus;

    public NewCharacterDBRequest(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(ushort authenticateId, Message message)
    {
        ushort gatewayId = message.GetUShort();
        ushort clientId = message.GetUShort();
        ObjectId userId = message.GetObjectId();
        string name = message.GetString();

        if (!MMOServer.TryGetClient(authenticateId, out var authenticate)) throw new InvalidOperationException("Gateway not found for specified clientId");
        var args = new NewCharacterDBRequestEvent(authenticate, gatewayId, clientId, userId, name);
        _eventBus.Publish(args);
    }
}