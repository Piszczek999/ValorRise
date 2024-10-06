using System.Numerics;
using MongoDB.Bson;
using Riptide;

namespace ValorRise.Server.Entities;

public abstract class Entity : IMessageSerializable
{
    public EntityType EntityType { get; set; }
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public float X { get; set; }
    public float Y { get; set; }

    public virtual void Deserialize(Message message)
    {
        EntityType = (EntityType)message.GetUShort();
        Id = message.GetObjectId();
        Name = message.GetString();
        X = message.GetFloat();
        Y = message.GetFloat();
    }

    public virtual void Serialize(Message message) => message
        .AddUShort((ushort)EntityType)
        .AddObjectId(Id)
        .AddString(Name)
        .AddFloat(X)
        .AddFloat(Y);
}