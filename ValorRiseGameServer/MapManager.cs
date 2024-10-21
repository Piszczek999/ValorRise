using System.Reflection;
using Newtonsoft.Json;
using ValorRise;

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
        var layer = tiledMap.Layers.FirstOrDefault(l => l.Name == "Walls");
        if (layer == null)
            throw new InvalidOperationException("Map does not contain a 'Walls' layer.");

        Width = layer.Width;
        Height = layer.Height;

        // Initialize the Tiles array using LINQ for simpler population
        Tiles = new bool[Height, Width];
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Tiles[y, x] = layer.Data[y * Width + x] != 0;
            }
        }
    }
}

public class MapManager
{
    public CollisionMap CollisionMap { get; private set; }

    public void LoadMap(ushort MapId)
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, $"Maps/map_{MapId}.json");
        Logger.Debug(filePath);

        try
        {
            // Read the JSON file
            string json = File.ReadAllText(filePath);

            // Deserialize the JSON into C# objects
            TiledMap map = JsonConvert.DeserializeObject<TiledMap>(json);
            CollisionMap = new CollisionMap(map);
        }
        catch (Exception ex)
        {
            Logger.Error($"Error loading map {MapId}: {ex.Message}\n{ex.StackTrace}");
        }
    }
}

public class Layer
{
    public int[] Data { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
}

public class TiledMap
{
    public int Height { get; set; }
    public int Width { get; set; }
    public List<Layer> Layers { get; set; }
    public int TileWidth { get; set; }
    public int TileHeight { get; set; }
}