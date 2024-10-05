using Riptide;

namespace ValorRise.Client.Messages;

internal class InitLevel : IMessageHandler
{
    private readonly EventBus _eventBus;

    public InitLevel(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        ushort mapId = message.GetUShort();

        var args = new InitLevelEvent(mapId);
        _eventBus.Publish(args);
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