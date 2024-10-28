using System.Numerics;
using MongoDB.Bson;
using ValorRise;
using ValorRise.Enums;

namespace ValorRiseGameServer.Entities;

public abstract class LivingEntity : Entity, IMoveable
{
    public float Health { get; protected set; }
    public float MaxHealth { get; protected set; }
    public bool IsDead { get; protected set; }
    public bool IsInvulnerable { get; protected set; }

    //IMoveable
    public Vector2 Destination { get; set; }
    public float Speed { get; protected set; }
    public bool IsMoving { get; protected set; }

    public LivingEntity(EntityType entityType, ObjectId id, string name, Vector2 position) : base(entityType, id, name, position)
    {
        Destination = position;
    }

    public LivingEntity(EntityType entityType, string name, Vector2 position) : base(entityType, name, position)
    {
        Destination = position;
    }

    public LivingEntity(EntityType entityType, Vector2 position) : base(entityType, position)
    {
        Destination = position;
    }

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

    public override void PhysicsUpdate(double delta)
    {
        UpdatePosition(delta);
    }

    protected void UpdatePosition(double deltaTime)
    {
        if (Position == Destination)
        {
            IsMoving = false;
            return;
        }

        IsMoving = true;
        var collisionMap = ValorServer.MapManager.CollisionMap;

        Vector2 toTarget = Destination - Position;
        float distanceToTarget = toTarget.Length();

        // If we're close enough to the target, stop moving
        if (distanceToTarget < 0.01f)
        {
            Position = Destination;
            return;
        }

        Vector2 direction = toTarget.Normalize();
        float distanceToMove = Speed * (float)deltaTime;
        Vector2 potentialPosition = Position + direction * Math.Min(distanceToMove, distanceToTarget);

        // Check for collisions along the X and Y axes
        bool xBlocked = collisionMap.CheckCollision(new Vector2(potentialPosition.X, Position.Y));
        bool yBlocked = collisionMap.CheckCollision(new Vector2(Position.X, potentialPosition.Y));

        // Adjust position based on collisions
        if (xBlocked && yBlocked)
        {
            Destination = Position;
            return;
        }
        else if (xBlocked)
        {
            if (direction.Y == 0) Destination = Position;
            else if (distanceToMove > Math.Abs(Position.Y - Destination.Y))
                if (!collisionMap.CheckCollision(new Vector2(Position.X, Destination.Y)))
                    Position = new Vector2(Position.X, Destination.Y);
                else
                    Destination = Position;
            else if (!collisionMap.CheckCollision(new Vector2(Position.X, direction.Y > 0 ? Position.Y + distanceToMove : Position.Y - distanceToMove)))
                Position = new Vector2(Position.X, direction.Y > 0 ? Position.Y + distanceToMove : Position.Y - distanceToMove);
            else
                Destination = Position;
        }
        else if (yBlocked)
        {
            if (direction.X == 0) Destination = Position;
            else if (distanceToMove > Math.Abs(Destination.X - Position.X))
                if (!collisionMap.CheckCollision(new Vector2(Destination.X, Position.Y)))
                    Position = new Vector2(Destination.X, Position.Y);
                else
                    Destination = Position;
            else if (!collisionMap.CheckCollision(new Vector2(direction.X > 0 ? Position.X + distanceToMove : Position.X - distanceToMove, Position.Y)))
                Position = new Vector2(direction.X > 0 ? Position.X + distanceToMove : Position.X - distanceToMove, Position.Y);
            else
                Destination = Position;
        }
        else
        {
            // Update position based on potential position
            Position = potentialPosition;
        }

    }
}