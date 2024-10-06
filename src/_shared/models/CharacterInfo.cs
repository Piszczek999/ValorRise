namespace ValorRise;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Riptide;

public class CharacterInfo : IMessageSerializable
{
    [BsonId] public ObjectId Id { get; set; }
    [BsonElement("ownerId")] public ObjectId OwnerId { get; set; }
    [BsonElement("name")] public string Name { get; set; }
    [BsonElement("level")] public int Level { get; set; }
    [BsonElement("exp")] public int Exp { get; set; }
    [BsonElement("createdAt")] public DateTime CreatedAt { get; set; }

    public void Serialize(Message message) => message
        .AddObjectId(Id)
        .AddObjectId(OwnerId)
        .AddString(Name)
        .AddInt(Level)
        .AddInt(Exp)
        .AddString(CreatedAt.ToString());

    public void Deserialize(Message message)
    {
        Id = message.GetObjectId();
        OwnerId = message.GetObjectId();
        Name = message.GetString();
        Level = message.GetInt();
        Exp = message.GetInt();
        CreatedAt = DateTime.Parse(message.GetString());
    }
}
