using Riptide;

namespace ValorRise.Server.Messages;

[Message((ushort)MessageType.ToAuthenticate.GameServerInfoRequest)]
internal class GameServerInfoRequest : IMessageHandler
{
    public void HandleMessage(ushort gameServerId, Message message)
    {
        string ipAddress = message.GetString();

        if (!MMOServer.TryGetClient(gameServerId, out var gameServer)) throw new InvalidOperationException("Gateway not found for specified clientId");
        var args = new GameServerInfoRequestEvent(gameServer, ipAddress);
        MMOServer.EventBus.Publish(args);
    }
}

public class GameServerInfoRequestEvent : EventArgs
{
    public Connection GameServer { get; }
    public string IpAddress { get; }

    public GameServerInfoRequestEvent(Connection gameServer, string ipAddress)
    {
        GameServer = gameServer;
        IpAddress = ipAddress;
    }
}