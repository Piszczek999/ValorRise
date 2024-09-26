namespace MMOLibrary;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


public class CharacterInfo
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

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }
}
