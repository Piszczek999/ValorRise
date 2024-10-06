using System.Numerics;
using MongoDB.Bson;
using Riptide;

namespace ValorRise.Client.Entities;

public abstract class Entity
{
    public EntityType EntityType { get; set; }
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public Vector2 Position { get; set; }

    public static Entity Deserialize(Message message)
    {
        EntityType entityType = (EntityType)message.GetUShort();
        Entity entity = entityType switch
        {
            EntityType.Player => PlayerEntity.Deserialize(message),
            _ => throw new InvalidOperationException(""),
        };
        return entity;
    }
}