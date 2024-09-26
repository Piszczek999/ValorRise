namespace MMOLibrary.Server;
using Riptide;

internal class NewCharacterRequestMessageHandler : IMessageHandler
{
    private readonly EventBus _eventBus;

    public NewCharacterRequestMessageHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(ushort clientId, Message message)
    {
        string name = message.GetString();

        if (!MMOServer.TryGetClient(clientId, out var client)) throw new InvalidOperationException("Client not found for specified clientId");
        var args = new NewCharacterRequestEvent(client, name);
        _eventBus.Publish(args);
    }
}