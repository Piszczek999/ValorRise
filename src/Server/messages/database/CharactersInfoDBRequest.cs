namespace MMOLibrary.Server.Messages;

using MongoDB.Bson;
using Riptide;

internal class CharactersInfoDBRequest : IMessageHandler
{
    private readonly EventBus _eventBus;

    public CharactersInfoDBRequest(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(ushort authenticateId, Message message)
    {
        ushort gatewayId = message.GetUShort();
        ushort clientId = message.GetUShort();
        ObjectId userId = message.GetObjectId();

        if (!MMOServer.TryGetClient(authenticateId, out var authenticate)) throw new InvalidOperationException("Gateway not found for specified clientId");
        var args = new CharactersInfoDBRequestEvent(authenticate, gatewayId, clientId, userId);
        _eventBus.Publish(args);
    }
}