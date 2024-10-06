using System.Numerics;
using MongoDB.Bson;
using Riptide;

namespace ValorRise.Server.Entities;

public abstract class Entity
{
    public EntityType EntityType { get; set; }
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public float X { get; set; }
    public float Y { get; set; }

    public virtual Message Serialize(Message message)
    {
        return message
        .AddUShort((ushort)EntityType)
        .AddObjectId(Id)
        .AddString(Name)
        .AddFloat(X)
        .AddFloat(Y);
    }
}