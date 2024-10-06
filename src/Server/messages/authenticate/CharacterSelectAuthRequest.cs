using MongoDB.Bson;
using Riptide;

namespace ValorRise.Server.Messages;

[Message((ushort)MessageType.ToAuthenticate.CharacterSelectAuthRequest)]
internal class CharacterSelectAuthRequest : IMessageHandler
{
    private GlobalEventHandler _eventHandler = MMOServer.GlobalEventHandler;

    public void HandleMessage(ushort gatewayId, Message message)
    {
        ushort clientId = message.GetUShort();
        ObjectId userId = message.GetObjectId();
        ObjectId characterId = message.GetObjectId();

        var args = new CharacterSelectAuthRequestEvent(gatewayId, clientId, userId, characterId);
        _eventHandler.InvokeEvent(args);
    }
}

public class CharacterSelectAuthRequestEvent : EventArgs
{
    public ushort GatewayId { get; }
    public Connection Gateway { get => MMOServer.TryGetClient(GatewayId, out var gateway) ? gateway : null; }
    public ushort ClientId { get; }
    public ObjectId UserId { get; }
    public ObjectId CharacterId { get; }

    public CharacterSelectAuthRequestEvent(ushort gatewayId, ushort clientId, ObjectId userId, ObjectId characterId)
    {
        GatewayId = gatewayId;
        ClientId = clientId;
        UserId = userId;
        CharacterId = characterId;
    }
}