namespace MMOLibrary.Server.Messages;

using MongoDB.Bson;
using Riptide;

internal class CharacterSelectDBRequest : IMessageHandler
{
    private readonly EventBus _eventBus;

    public CharacterSelectDBRequest(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(ushort authenticateId, Message message)
    {
        ushort gatewayId = message.GetUShort();
        ushort clientId = message.GetUShort();
        ObjectId userId = message.GetObjectId();
        ObjectId characterId = message.GetObjectId();

        if (!MMOServer.TryGetClient(authenticateId, out var authenticate)) throw new InvalidOperationException("Gateway not found for specified clientId");
        var args = new CharacterSelectDBRequestEvent(authenticate, gatewayId, clientId, userId, characterId);
        _eventBus.Publish(args);
    }
}