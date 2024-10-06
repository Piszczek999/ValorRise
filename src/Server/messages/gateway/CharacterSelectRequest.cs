using MongoDB.Bson;
using Riptide;

namespace ValorRise.Server.Messages;

[Message((ushort)MessageType.ToGateway.CharacterSelectRequest)]
internal class CharacterSelectRequest : IMessageHandler
{
    private GlobalEventHandler _eventHandler = MMOServer.GlobalEventHandler;

    public void HandleMessage(ushort clientId, Message message)
    {
        ObjectId characterId = message.GetObjectId();

        var args = new CharacterSelectRequestEvent(clientId, characterId);
        _eventHandler.InvokeEvent(args);
    }
}

public class CharacterSelectRequestEvent : EventArgs
{
    public ushort ClientId { get; }
    public Connection Client { get => MMOServer.TryGetClient(ClientId, out var client) ? client : null; }
    public ObjectId CharacterId { get; }

    public CharacterSelectRequestEvent(ushort clientId, ObjectId characterId)
    {
        ClientId = clientId;
        CharacterId = characterId;
    }
}