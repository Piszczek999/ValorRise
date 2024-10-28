using System.Numerics;
using ValorRise;
using ValorRise.Enums;
using ValorRiseGameServer.Entities;

namespace ValorRiseGameServer;

public class EntitySpawner
{
    public EntityType EntityType { get; set; }
    public Vector2 Position { get; set; }
    public short Radius { get; set; }
    public short Count { get; set; }
    public int CooldownInSeconds { get; set; }
    private bool _isCleared;
    private double _remainingCooldown;
    private readonly Entity[] _entities;
    private static readonly Random _random = new();
    private const int MaxSpawnAttempts = 20;

    public EntitySpawner(EntityType entityType, Vector2 position, short radius, short count, int cooldownInSeconds)
    {
        EntityType = entityType;
        Position = position;
        Radius = radius;
        Count = count;
        CooldownInSeconds = cooldownInSeconds;
        _entities = new Entity[count];
        SpawnEntities();
    }

    public void SpawnEntities()
    {
        for (int i = 0; i < Count; i++)
        {
            Vector2 spawnPosition = Position;
            int attempt = 0;
            var entity = CreateEntity(EntityType, spawnPosition);
            // Try finding a valid position within the radius
            while (attempt < MaxSpawnAttempts)
            {
                double angle = _random.NextDouble() * Math.PI * 2;
                double distance = _random.NextDouble() * Radius;

                spawnPosition = Position + new Vector2(
                    (float)(distance * Math.Cos(angle)),
                    (float)(distance * Math.Sin(angle))
                );

                if (!ValorServer.MapManager.CollisionMap.CheckCollision(spawnPosition))
                    break;

                attempt++;
            }

            if (attempt < MaxSpawnAttempts)
            {
                entity.Position = spawnPosition;
                _entities[i] = entity;
                ValorServer.EntityManager.AddEntity(entity);
            }
            else
            {
                Logger.Warning($"Could not find a valid spawn position for entity {i} within the radius.");
            }
        }
    }

    public void Update(double delta)
    {
        if (_isCleared)
        {
            _remainingCooldown = -delta;
            if (_remainingCooldown <= 0)
            {
                SpawnEntities();
                _isCleared = false;
            }
        }
        else if (_entities.All(e => e == null || !e.IsActive))
        {
            _isCleared = true;
            _remainingCooldown = CooldownInSeconds;
        }
    }

    private Entity CreateEntity(EntityType type, Vector2 position)
    {
        return type switch
        {
            EntityType.Slime => new Slime(position),
            // EntityType.Worm => new Worm(),
            // EntityType.Zombie => new Zombie(),
            _ => throw new ArgumentException("Unknown entity type", nameof(type))
        };
    }
}