using MongoDB.Bson;
using Riptide;

namespace ValorRise.Client.Messages;

[Message((ushort)MessageType.ToClient.EntityMove)]
internal class EntityMove : IMessageHandler
{
    private GlobalEventHandler _eventHandler = MMOClient.GlobalEventHandler;

    public void HandleMessage(Message message)
    {
        ObjectId entityId = message.GetObjectId();
        float x = message.GetFloat();
        float y = message.GetFloat();

        var args = new EntityMoveEvent(entityId, x, y);
        _eventHandler.InvokeEvent(args);
    }
}

public class EntityMoveEvent : EventArgs
{
    public ObjectId EntityId { get; }
    public float X { get; }
    public float Y { get; }

    public EntityMoveEvent(ObjectId entityId, float x, float y)
    {
        EntityId = entityId;
        X = x;
        Y = y;
    }
}