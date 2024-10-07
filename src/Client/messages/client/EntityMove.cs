using System.Numerics;
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
        Vector2 newPosition = message.GetVector2();

        var args = new EntityMoveEvent(entityId, newPosition);
        _eventHandler.InvokeEvent(args);
    }
}

public class EntityMoveEvent : EventArgs
{
    public ObjectId EntityId { get; }
    public Vector2 NewPosition { get; }

    public EntityMoveEvent(ObjectId entityId, Vector2 newPosition)
    {
        EntityId = entityId;
        NewPosition = newPosition;
    }
}