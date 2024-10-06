using Riptide;

namespace ValorRise.Server.Messages;

[Message((ushort)MessageType.ToGameServer.PlayerMove)]
internal class PlayerMove : IMessageHandler
{
    private GlobalEventHandler _eventHandler = MMOServer.GlobalEventHandler;

    public void HandleMessage(ushort clientId, Message message)
    {
        float x = message.GetFloat();
        float y = message.GetFloat();

        var args = new PlayerMoveEvent(clientId, x, y);
        _eventHandler.InvokeEvent(args);
    }
}

public class PlayerMoveEvent : EventArgs
{
    public ushort ClientId { get; }
    public Connection Client { get => MMOServer.TryGetClient(ClientId, out var client) ? client : null; }
    public float X { get; }
    public float Y { get; }

    public PlayerMoveEvent(ushort clientId, float x, float y)
    {
        ClientId = clientId;
        X = x;
        Y = y;
    }
}