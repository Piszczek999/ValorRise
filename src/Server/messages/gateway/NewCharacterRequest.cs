namespace ValorRiseServer.Messages;

using ValorRiseClient;
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

public class NewCharacterRequestEvent : EventArgs
{
    public Connection Client { get; }
    public string Name { get; }

    public NewCharacterRequestEvent(Connection client, string name)
    {
        Client = client;
        Name = name;
    }
}