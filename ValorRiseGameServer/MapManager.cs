using System.Numerics;
using Newtonsoft.Json;
using ValorRise;
using ValorRise.Enums;
using ValorRiseGameServer.Entities;

namespace ValorRiseGameServer;

public class CollisionMap
{
    public bool[,] Tiles { get; private set; }
    public int TileSize { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }

    public CollisionMap(TiledMap tiledMap)
    {
        TileSize = tiledMap.TileWidth;

        // Use FirstOrDefault to avoid exceptions when the layer is missing
        var wallsLayer = tiledMap.Layers.FirstOrDefault(l => l.Name == "Walls");
        if (wallsLayer == null)
            throw new InvalidOperationException("Map does not contain a 'Walls' layer.");

        Width = wallsLayer.Width;
        Height = wallsLayer.Height;

        Tiles = new bool[Height, Width];
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Tiles[y, x] = wallsLayer.Data[y * Width + x] != 0;
            }
        }
    }

    public bool CheckCollision(Vector2 position)
    {
        return Tiles[(int)(position.Y / TileSize), (int)(position.X / TileSize)];
    }
}

public class MapManager
{
    public CollisionMap CollisionMap { get; private set; }
    public List<EntitySpawner> Spawners { get; set; } = new();

    public void LoadMap(ushort MapId)
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, $"Maps/map_{MapId}.json");

        try
        {
            string json = File.ReadAllText(filePath);

            TiledMap map = JsonConvert.DeserializeObject<TiledMap>(json);
            CollisionMap = new CollisionMap(map);

            var entityLayers = map.Layers.Where(l => l.Name.StartsWith("entity_"));
            foreach (var layer in entityLayers)
            {

                string entityTypeName = layer.Name.Substring("entity_".Length);
                if (Enum.TryParse(typeof(EntityType), entityTypeName, true, out var entityType))
                {
                    foreach (var obj in layer.Objects)
                    {
                        var spawner = CreateSpawner((EntityType)entityType, obj);
                        if (spawner != null)
                        {
                            Spawners.Add(spawner);
                        }
                        else
                        {
                            Logger.Warning("Spawner not added: " + obj.Name);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Error($"Error loading map {MapId}: {ex.Message}\n{ex.StackTrace}");
        }
    }

    public void Update(double delta)
    {
        foreach (var spawner in Spawners)
            spawner.Update(delta);
    }

    private EntitySpawner CreateSpawner(EntityType entityType, Object obj)
    {
        return entityType switch
        {
            EntityType.Slime => new EntitySpawner(
                entityType,
                new Vector2(obj.X, obj.Y),
                radius: 50,
                count: 3,
                cooldownInSeconds: 5
            ),
            // EntityType.Goblin => new EntitySpawner(entityType, count: 2, cooldown: 7000)
            // {
            //     Position = new Vector2(obj.X, obj.Y),
            //     Radius = 7
            // },
            _ => throw new InvalidOperationException($"Unhandled entity type: {entityType}")
        };
    }
}

public class Layer
{
    public int[] Data { get; set; }
    public int Id { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public List<Object> Objects { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
}

public class TiledMap
{
    public int Height { get; set; }
    public int Width { get; set; }
    public List<Layer> Layers { get; set; }
    public int TileWidth { get; set; }
    public int TileHeight { get; set; }
}

public class Object
{
    public int Height { get; set; }
    public int Width { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Point { get; set; }
    public int Rotation { get; set; }
    public string Type { get; set; }
    public bool Visible { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
}