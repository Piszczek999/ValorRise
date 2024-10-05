using MongoDB.Bson;
using Riptide;

namespace ValorRise.Server.Messages;

[Message((ushort)MessageType.ToGateway.CharacterSelectRequest)]
internal class CharacterSelectRequest : IMessageHandler
{
    public void HandleMessage(ushort clientId, Message message)
    {
        ObjectId characterId = message.GetObjectId();

        if (!MMOServer.TryGetClient(clientId, out var client)) throw new InvalidOperationException("Client not found for specified clientId");
        var args = new CharacterSelectRequestEvent(client, characterId);
        MMOServer.EventBus.Publish(args);
    }
}

public class CharacterSelectRequestEvent : EventArgs
{
    public Connection Client { get; }
    public ObjectId CharacterId { get; }

    public CharacterSelectRequestEvent(Connection client, ObjectId characterId)
    {
        Client = client;
        CharacterId = characterId;
    }
}