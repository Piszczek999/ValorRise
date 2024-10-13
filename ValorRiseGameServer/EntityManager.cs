using MongoDB.Bson;
using ValorRiseGameServer.Entities;

namespace ValorRiseGameServer;

public class EntityManager : IEntityManager
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
}