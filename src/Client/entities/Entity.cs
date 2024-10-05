using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise;
using ValorRise.Entities;

namespace ValorRiseClient;

public abstract class Entity : IEntity
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
            EntityType.Player => Player.Deserialize(message),
            _ => throw new InvalidOperationException(""),
        };
        return entity;
    }
}