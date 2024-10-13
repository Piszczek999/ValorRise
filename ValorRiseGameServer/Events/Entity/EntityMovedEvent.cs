using ValorRiseGameServer.Entities;

namespace ValorRiseGameServer.Events;

public class EntityMoveEvent : IEntityEvent, ICancellableEvent
{
    public Entity Entity { get; }

    public bool IsCancelled { get; set; }

    public EntityMoveEvent(Entity entity)
    {
        Entity = entity;
    }
}