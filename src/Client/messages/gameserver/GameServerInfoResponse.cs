using Riptide;

namespace ValorRise.Client.Messages;

[Message((ushort)MessageType.ToGameServer.GameServerInfoResponse)]
internal class GameServerInfoResponse : IMessageHandler
{
    public void HandleMessage(Message message)
    {
        ushort mapId = message.GetUShort();
        ushort port = message.GetUShort();

        var args = new GameServerInfoResponseEvent(mapId, port);
        MMOClient.EventBus.Publish(args);
    }
}

public class GameServerInfoResponseEvent : EventArgs
{
    public ushort MapId { get; }
    public ushort Port { get; }

    public GameServerInfoResponseEvent(ushort mapId, ushort port)
    {
        MapId = mapId;
        Port = port;
    }
}