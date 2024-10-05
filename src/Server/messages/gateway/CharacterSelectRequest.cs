namespace ValorRiseServer.Messages;

using ValorRiseClient;
using MongoDB.Bson;
using Riptide;

internal class CharacterSelectRequest : IMessageHandler
{
    private readonly EventBus _eventBus;

    public CharacterSelectRequest(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(ushort clientId, Message message)
    {
        ObjectId characterId = message.GetObjectId();

        if (!MMOServer.TryGetClient(clientId, out var client)) throw new InvalidOperationException("Client not found for specified clientId");
        var args = new CharacterSelectRequestEvent(client, characterId);
        _eventBus.Publish(args);
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