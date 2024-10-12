using ValorRiseServer.Entities;

namespace ValorRiseServer;

public class EntityMoveEvent : IEntityEvent
{
    public Entity Entity { get; }

    public EntityMoveEvent(Entity entity)
    {
        Entity = entity;
    }
}