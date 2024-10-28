using System.Numerics;
using MongoDB.Bson;
using ValorRise.Enums;

namespace ValorRiseGameServer.Entities;

public abstract class Entity : Updatable
{
    public EntityType EntityType { get; set; }
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public long LastUpdateTimestamp { get; set; }
    public Vector2 Position { get; set; }
    protected Vector2 _spawnPosition;
    public bool IsVisible { get; set; } = true;
    public bool IsActive { get; set; } = true;
    public bool IsCollidable { get; protected set; } = true;
    public float CollisionRadius { get; protected set; } = 0;

    public Entity(EntityType type, ObjectId id, string name, Vector2 position)
    {
        EntityType = type;
        Id = id;
        Name = name;
        Position = position;
        _spawnPosition = position;
    }

    public Entity(EntityType type, string name, Vector2 position)
    {
        EntityType = type;
        Id = ObjectId.GenerateNewId();
        Name = name;
        Position = position;
        _spawnPosition = position;
    }

    public Entity(EntityType type, Vector2 position)
    {
        EntityType = type;
        Id = ObjectId.GenerateNewId();
        Name = "";
        Position = position;
        _spawnPosition = position;
    }
}