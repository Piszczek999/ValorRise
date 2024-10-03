namespace MMOLibrary.Server.Messages;
using Riptide;

internal class NewCharacterRequest : IMessageHandler
{
    private readonly EventBus _eventBus;

    public NewCharacterRequest(EventBus eventBus)
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