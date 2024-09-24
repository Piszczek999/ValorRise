namespace MMO_Library.Server;
using Riptide;

internal class NewCharacterRequestMessage : IMessageHandler
{
    private readonly EventBus _eventBus;
    private readonly ConnectionManager _clientManager;

    public NewCharacterRequestMessage(EventBus eventBus, ConnectionManager clientManager)
    {
        _eventBus = eventBus;
        _clientManager = clientManager;
    }

    public void HandleMessage(ushort clientId, Message message)
    {
        string name = message.GetString();

        var client = _clientManager.GetConnection(clientId);
        var args = new NewCharacterRequestEvent(client, name);
        _eventBus.Publish(args);
    }
}