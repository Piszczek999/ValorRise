using Riptide;

namespace ValorRise.Server.Messages;

[Message((ushort)MessageType.ToGateway.NewCharacterRequest)]
internal class NewCharacterRequest : IMessageHandler
{
    public void HandleMessage(ushort clientId, Message message)
    {
        string name = message.GetString();

        if (!MMOServer.TryGetClient(clientId, out var client)) throw new InvalidOperationException("Client not found for specified clientId");
        var args = new NewCharacterRequestEvent(client, name);
        MMOServer.EventBus.Publish(args);
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