using System.Numerics;
using MongoDB.Bson;
using ValorRise.Enums;

namespace ValorRiseServer.Entities;

public abstract class LivingEntity : Entity, IMoveable
{
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public bool IsDead { get; set; }
    public bool IsInvulnerable { get; set; }
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
        if (distanceToMove >= distanceToTarget)
        {
            Position = Destination;
        }
        else
        {
            // Move towards the target
            Position += direction * distanceToMove;
        }
        return true;
    }
}