using MongoDB.Bson;
using ValorRise;
using ValorRise.Packets.Play.Server;
using ValorRiseGameServer.Entities;

namespace ValorRiseGameServer;

public class EntityManager : Updatable
{
    private readonly Dictionary<ObjectId, Entity> _entities = new();

    public Entity AddEntity(Entity entity)
    {
        _entities.Add(entity.Id, entity);
        return entity;
    }

    public bool RemoveEntity(ObjectId id)
    {
        return _entities.Remove(id);
    }

    public Entity GetEntity(ObjectId id)
    {
        return _entities.TryGetValue(id, out Entity entity) ? entity : null;
    }

    public IEnumerable<Entity> GetEntities()
    {
        return _entities.Values;
    }

    public override void PhysicsUpdate(double delta)
    {
        foreach (var entity in _entities.Values)
            entity.PhysicsUpdate(delta);

        var entityStates = _entities.Values
            .Where(e =>
                e.IsVisible &&
                e is IMoveable moveable &&
                moveable.IsMoving)
            .Select(e => new EntityState { Id = e.Id, Position = e.Position })
            .ToArray();

        ValorServer.SendToAll(new WorldStatePacket(entityStates));
    }
}