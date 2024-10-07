using System.Numerics;
using MongoDB.Bson;
using Riptide;

namespace ValorRise.Server.Entities;

public abstract class Entity : IMessageSerializable
{
    public EntityType EntityType { get; set; }
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Destination { get; set; }
    public float Speed { get; set; } = 10.0f;

    public virtual void Deserialize(Message message)
    {
        EntityType = (EntityType)message.GetUShort();
        Id = message.GetObjectId();
        Name = message.GetString();
        Position = message.GetVector2();
        Destination = message.GetVector2();
        Speed = message.GetFloat();
    }

    public virtual void Serialize(Message message) => message
        .AddUShort((ushort)EntityType)
        .AddObjectId(Id)
        .AddString(Name)
        .AddVector2(Position)
        .AddVector2(Destination)
        .AddFloat(Speed);

    public void Move(double deltaTime)
    {
        // Calculate direction to the target
        Vector2 direction = Destination - Position;
        float distanceToTarget = direction.Length();

        // If we're already at the target, stop moving
        if (distanceToTarget < 0.01f) // Small threshold to avoid floating point issues
        {
            Position = Destination;
            return;
        }

        direction /= distanceToTarget;
        float distanceToMove = Speed * (float)deltaTime;

        // Move in the direction of the target, but do not overshoot
        if (distanceToMove >= distanceToTarget)
        {
            Position = Destination;
        }
        else
        {
            // Move towards the target
            Position += direction * distanceToMove;
        }
    }
}