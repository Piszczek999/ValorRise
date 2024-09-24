namespace MMO_Library.Server;
using MongoDB.Bson;
using Riptide;

internal class CharacterSelectAuthRequestMessageHandler : IMessageHandler
{
    private readonly EventBus _eventBus;
    private readonly Server _server;

    public CharacterSelectAuthRequestMessageHandler(EventBus eventBus, Server server)
    {
        _eventBus = eventBus;
        _server = server;
    }

    public void HandleMessage(ushort gatewayId, Message message)
    {
        ushort clientId = message.GetUShort();
        ObjectId userId = message.GetObjectId();
        ObjectId characterId = message.GetObjectId();

        if (!_server.TryGetClient(clientId, out var client)) throw new InvalidOperationException("Client not found for specified clientId");
        var args = new CharacterSelectRequestEvent(client, characterId);
        _eventBus.Publish(args);
    }
}