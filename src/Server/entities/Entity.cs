using System.Numerics;
using MongoDB.Bson;

public class Entity
{
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public Vector2 Position { get; set; }
}