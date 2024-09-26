namespace MMOLibrary.Server;
using MongoDB.Bson;
using Riptide;

internal class CharacterSelectRequestMessageHandler : IMessageHandler
{
    private readonly EventBus _eventBus;

    public CharacterSelectRequestMessageHandler(EventBus eventBus)
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