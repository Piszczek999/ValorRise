using System.Numerics;
using MongoDB.Bson;
using Riptide;

namespace MMOLibrary.Server;

public abstract class Entity
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