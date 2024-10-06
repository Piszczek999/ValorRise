using Riptide;
using ValorRise.Client.Entities;

namespace ValorRise.Client.Messages;

[Message((ushort)MessageType.ToClient.SpawnEntity)]
internal class SpawnEntity : IMessageHandler
{
    private GlobalEventHandler _eventHandler = MMOClient.GlobalEventHandler;

    public void HandleMessage(Message message)
    {
        Entity entity = Entity.Deserialize(message);

        var args = new SpawnEntityEvent(entity);
        _eventHandler.InvokeEvent(args);
    }
}

public class SpawnEntityEvent : EventArgs
{
    public Entity Entity { get; }

    public SpawnEntityEvent(Entity entity)
    {
        Entity = entity;
    }
}