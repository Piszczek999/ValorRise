using ValorRiseServer.Entities;

namespace ValorRiseServer;

public class EntityMovedEvent : IEntityEvent
{
    public Entity Entity { get; }

    public EntityMovedEvent(Entity entity)
    {
        Entity = entity;
    }
}