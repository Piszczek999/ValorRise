using System.Numerics;
using MongoDB.Bson;
using Riptide;

namespace ValorRise.Server.Entities;

public abstract class Entity
{
    public EntityType EntityType { get; set; }
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public Vector2 Position { get; set; }

    public virtual void Serialize(Message message)
    {
        message.AddUShort((ushort)EntityType);
        message.AddObjectId(Id);
        message.AddString(Name);
        message.AddVector2(Position);
    }
}