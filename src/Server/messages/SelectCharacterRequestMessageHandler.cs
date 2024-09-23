namespace MMO_Library.Server;
using MongoDB.Bson;
using Riptide;

internal class SelectCharacterRequestMessageHandler : IMessageHandler
{
    private readonly EventBus _eventBus;
    private readonly ConnectionManager _clientManager;

    public SelectCharacterRequestMessageHandler(EventBus eventBus, ConnectionManager clientManager)
    {
        _eventBus = eventBus;
        _clientManager = clientManager;
    }

    public void HandleMessage(ushort clientId, Message message)
    {
        ObjectId characterId = message.GetObjectId();

        var client = _clientManager.GetConnection(clientId);
        var args = new SelectCharacterRequestEvent(client, characterId);
        _eventBus.Publish(args);
    }
}