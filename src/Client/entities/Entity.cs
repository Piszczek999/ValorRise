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

    public virtual void Serialize(Message message) => message
        .AddUShort((ushort)EntityType)
        .AddObjectId(Id)
        .AddString(Name)
        .AddVector2(Position);

    public static Entity Deserialize(Message message)
    {
        EntityType entityType = (EntityType)message.GetUShort();
        return entityType switch
        {
            EntityType.Player => PlayerEntity.Deserialize(message),
            _ => throw new InvalidOperationException("Unsupported entity type"),
        };
    }
}