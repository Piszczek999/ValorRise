namespace ValorRise;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


public class Character
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("ownerId")]
    public ObjectId OwnerId { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("level")]
    public int Level { get; set; }

    [BsonElement("exp")]
    public int Exp { get; set; }

    [BsonElement("x")]
    public double X { get; set; }

    [BsonElement("y")]
    public double Y { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }
}
