using MongoDB.Bson;
using Riptide;

namespace ValorRise.Server.Messages;

internal class CharacterSelectAuthRequest : IMessageHandler
{
    private readonly EventBus _eventBus;

    public CharacterSelectAuthRequest(EventBus eventBus)
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

public class CharacterSelectAuthRequestEvent : EventArgs
{
    public Connection Gateway { get; }
    public ushort ClientId { get; }
    public ObjectId UserId { get; }
    public ObjectId CharacterId { get; }

    public CharacterSelectAuthRequestEvent(Connection gateway, ushort clientId, ObjectId userId, ObjectId characterId)
    {
        Gateway = gateway;
        ClientId = clientId;
        UserId = userId;
        CharacterId = characterId;
    }
}