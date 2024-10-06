namespace ValorRise;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Riptide;

public class Character : IMessageSerializable
{
    [BsonId] public ObjectId Id { get; set; }
    [BsonElement("ownerId")] public ObjectId OwnerId { get; set; }
    [BsonElement("name")] public string Name { get; set; }
    [BsonElement("level")] public int Level { get; set; }
    [BsonElement("exp")] public int Exp { get; set; }
    [BsonElement("mapId")] public ushort MapId { get; set; }
    [BsonElement("x")] public float X { get; set; }
    [BsonElement("y")] public float Y { get; set; }
    [BsonElement("health")] public float Health { get; set; }
    [BsonElement("maxHealth")] public float MaxHealth { get; set; }
    [BsonElement("mana")] public float Mana { get; set; }
    [BsonElement("maxMana")] public float MaxMana { get; set; }
    [BsonElement("createdAt")] public DateTime CreatedAt { get; set; }

    public void Serialize(Message message) => message
            .AddObjectId(Id)
            .AddObjectId(OwnerId)
            .AddString(Name)
            .AddInt(Level)
            .AddInt(Exp)
            .AddUShort(MapId)
            .AddFloat(X)
            .AddFloat(Y)
            .AddFloat(Health)
            .AddFloat(MaxHealth)
            .AddFloat(Mana)
            .AddFloat(MaxMana)
            .AddString(CreatedAt.ToString());

    public void Deserialize(Message message)
    {
        Id = message.GetObjectId();
        OwnerId = message.GetObjectId();
        Name = message.GetString();
        Level = message.GetInt();
        Exp = message.GetInt();
        MapId = message.GetUShort();
        X = message.GetFloat();
        Y = message.GetFloat();
        Health = message.GetFloat();
        MaxHealth = message.GetFloat();
        Mana = message.GetFloat();
        MaxMana = message.GetFloat();
        CreatedAt = DateTime.Parse(message.GetString());
    }
}
