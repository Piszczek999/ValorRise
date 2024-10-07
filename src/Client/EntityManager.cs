using MongoDB.Bson;
using ValorRise.Client.Entities;

namespace ValorRise.Client;

public class EntityManager
{
    private readonly Dictionary<ObjectId, Entity> _entities = new();

    public Entity AddEntity(Entity entity)
    {
        _entities.Add(entity.Id, entity);
        return entity;
    }

    public void RemoveEntity(ObjectId id)
    {
        _entities.Remove(id);
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