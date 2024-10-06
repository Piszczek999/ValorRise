using Riptide;

namespace ValorRise.Server.Messages;

[Message((ushort)MessageType.ToGateway.NewCharacterRequest)]
internal class NewCharacterRequest : IMessageHandler
{
    private GlobalEventHandler _eventHandler = MMOServer.GlobalEventHandler;

    public void HandleMessage(ushort clientId, Message message)
    {
        string name = message.GetString();

        var args = new NewCharacterRequestEvent(clientId, name);
        _eventHandler.InvokeEvent(args);
    }
}

public class NewCharacterRequestEvent : EventArgs
{
    public ushort ClientId { get; }
    public Connection Client { get => MMOServer.TryGetClient(ClientId, out var client) ? client : null; }
    public string Name { get; }

    public NewCharacterRequestEvent(ushort clientId, string name)
    {
        ClientId = clientId;
        Name = name;
    }
}