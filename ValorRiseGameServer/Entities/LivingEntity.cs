using System.Numerics;
using MongoDB.Bson;
using ValorRise.Enums;

namespace ValorRiseGameServer.Entities;

public abstract class LivingEntity : Entity, IMoveable
{
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public bool IsDead { get; set; }
    public bool IsInvulnerable { get; set; }

    //IMoveable
    public Vector2 Destination { get; set; }
    public bool IsCollidable { get; set; }
    public float Speed { get; set; }

    public LivingEntity(EntityType type, ObjectId id) : base(type, id) { }
    public LivingEntity(EntityType type) : base(type, ObjectId.GenerateNewId()) { }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
            Die();
    }

    public void Die()
    {
        // Implement death behavior
    }

    public void Heal(float healAmount)
    {
        Health = Math.Min(Health + healAmount, MaxHealth);
    }

    public virtual bool UpdatePosition(double deltaTime)
    {
        var collisionMap = ValorServer.MapManager.CollisionMap;

        Vector2 toTarget = Destination - Position;

        float distanceToTarget = toTarget.Length();

        // If we're already at the target, stop moving
        if (distanceToTarget < 0.01f) // Small threshold to avoid floating point issues
        {
            Position = Destination;
            return false; // No movement occurred
        }

        Vector2 direction = toTarget / distanceToTarget;

        // Calculate how far we can move in this frame
        float distanceToMove = Speed * (float)deltaTime;

        // Move in the direction of the target, but do not overshoot
        Vector2 potentialPosition;
        if (distanceToMove >= distanceToTarget)
        {
            potentialPosition = Destination;
        }
        else
        {
            potentialPosition = Position + direction * distanceToMove;
        }

        // Check if the target position collides with walls
        bool isBlocked(Vector2 position) =>
            collisionMap.Tiles[(int)(position.Y / collisionMap.TileSize), (int)(position.X / collisionMap.TileSize)];

        bool xBlocked = isBlocked(new Vector2(potentialPosition.X, Position.Y));
        bool yBlocked = isBlocked(new Vector2(Position.X, potentialPosition.Y));

        if (xBlocked && yBlocked)
        {
            // Both directions blocked, stop movement
            return false;
        }
        else if (xBlocked)
        {
            potentialPosition.X = Position.X; // Slide along Y-axis
        }
        else if (yBlocked)
        {
            potentialPosition.Y = Position.Y; // Slide along X-axis
        }

        // No collision or sliding is possible, update position
        Position = potentialPosition;
        return true;
    }
}