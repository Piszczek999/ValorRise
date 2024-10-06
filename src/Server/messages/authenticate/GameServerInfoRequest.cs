using Riptide;

namespace ValorRise.Server.Messages;

[Message((ushort)MessageType.ToAuthenticate.GameServerInfoRequest)]
internal class GameServerInfoRequest : IMessageHandler
{
    private GlobalEventHandler _eventHandler = MMOServer.GlobalEventHandler;

    public void HandleMessage(ushort gameServerId, Message message)
    {
        string ipAddress = message.GetString();

        var args = new GameServerInfoRequestEvent(gameServerId, ipAddress);
        _eventHandler.InvokeEvent(args);
    }
}

public class GameServerInfoRequestEvent : EventArgs
{
    public ushort GameServerId { get; }
    public Connection GameServer { get => MMOServer.TryGetClient(GameServerId, out var gameServer) ? gameServer : null; }
    public string IpAddress { get; }

    public GameServerInfoRequestEvent(ushort gameServerId, string ipAddress)
    {
        GameServerId = gameServerId;
        IpAddress = ipAddress;
    }
}