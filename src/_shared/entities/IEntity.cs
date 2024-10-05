using System.Numerics;
using ValorRiseClient;
using MongoDB.Bson;
using Riptide;

namespace ValorRise.Entities;

public interface IEntity
{
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public Vector2 Position { get; set; }

    public virtual void Serialize(Message message)
    {
        message.AddObjectId(Id);
        message.AddString(Name);
        message.AddVector2(Position);
    }
}