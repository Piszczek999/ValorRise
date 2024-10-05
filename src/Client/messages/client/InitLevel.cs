using Riptide;

namespace ValorRise.Client.Messages;

[Message((ushort)MessageType.ToClient.InitLevel)]
internal class InitLevel : IMessageHandler
{
    public void HandleMessage(Message message)
    {
        ushort mapId = message.GetUShort();

        var args = new InitLevelEvent(mapId);
        MMOClient.EventBus.Publish(args);
    }
}

public class InitLevelEvent : EventArgs
{
    public ushort MapId { get; }

    public InitLevelEvent(ushort mapId)
    {
        MapId = mapId;
    }
}