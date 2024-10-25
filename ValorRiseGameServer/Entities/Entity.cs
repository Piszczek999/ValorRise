using System.Numerics;
using MongoDB.Bson;
using ValorRise.Enums;

namespace ValorRiseGameServer.Entities;

public abstract class Entity
{
    public EntityType EntityType { get; set; }
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public long LastUpdateTimestamp { get; set; }
    public Vector2 Position { get; set; }

    public Entity(EntityType type, ObjectId id)
    {
        EntityType = type;
        Id = id;
    }
}