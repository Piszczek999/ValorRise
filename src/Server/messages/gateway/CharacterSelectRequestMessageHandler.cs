namespace MMOLibrary.Server;
using MongoDB.Bson;
using Riptide;

internal class CharacterSelectRequestMessageHandler : IMessageHandler
{
    private readonly EventBus _eventBus;
    private readonly MMOServer _server;

    public CharacterSelectRequestMessageHandler(EventBus eventBus, MMOServer server)
    {
        _eventBus = eventBus;
        _server = server;
    }

    public void HandleMessage(ushort clientId, Message message)
    {
        ObjectId characterId = message.GetObjectId();

        if (!_server.TryGetClient(clientId, out var client)) throw new InvalidOperationException("Client not found for specified clientId");
        var args = new CharacterSelectRequestEvent(client, characterId);
        _eventBus.Publish(args);
    }
}