namespace MMO_Library.Server;
using Riptide;

internal class NewCharacterRequestMessageHandler : IMessageHandler
{
    private readonly EventBus _eventBus;
    private readonly Server _server;

    public NewCharacterRequestMessageHandler(EventBus eventBus, Server server)
    {
        _eventBus = eventBus;
        _server = server;
    }

    public void HandleMessage(ushort clientId, Message message)
    {
        string name = message.GetString();

        if (!_server.TryGetClient(clientId, out var client)) throw new InvalidOperationException("Client not found for specified clientId");
        var args = new NewCharacterRequestEvent(client, name);
        _eventBus.Publish(args);
    }
}