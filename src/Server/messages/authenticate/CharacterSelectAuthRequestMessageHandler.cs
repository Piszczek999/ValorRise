namespace MMOLibrary.Server;
using MongoDB.Bson;
using Riptide;

internal class CharacterSelectAuthRequestMessageHandler : IMessageHandler
{
    private readonly EventBus _eventBus;

    public CharacterSelectAuthRequestMessageHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(ushort gatewayId, Message message)
    {
        ushort clientId = message.GetUShort();
        ObjectId userId = message.GetObjectId();
        ObjectId characterId = message.GetObjectId();

        if (!MMOServer.TryGetClient(gatewayId, out var gateway)) throw new InvalidOperationException("Gateway not found for specified clientId");
        var args = new CharacterSelectAuthRequestEvent(gateway, clientId, userId, characterId);
        _eventBus.Publish(args);
    }
}