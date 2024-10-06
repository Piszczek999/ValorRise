namespace ValorRise;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Riptide;

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

    [BsonElement("mapId")]
    public ushort MapId { get; set; }

    [BsonElement("x")]
    public double X { get; set; }

    [BsonElement("y")]
    public double Y { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    public Message Serialize(Message message)
    {
        return message
            .AddObjectId(Id)
            .AddObjectId(OwnerId)
            .AddString(Name)
            .AddInt(Level)
            .AddInt(Exp)
            .AddUShort(MapId)
            .AddDouble(X)
            .AddDouble(Y)
            .AddString(CreatedAt.ToString());
    }

    public static Character Deserialize(Message message)
    {
        return new Character()
        {
            Id = message.GetObjectId(),
            OwnerId = message.GetObjectId(),
            Name = message.GetString(),
            Level = message.GetInt(),
            Exp = message.GetInt(),
            MapId = message.GetUShort(),
            X = message.GetDouble(),
            Y = message.GetDouble(),
            CreatedAt = DateTime.Parse(message.GetString())
        };
    }

    public static Character[] DeserializeMany(Message message)
    {
        var count = message.GetUShort();
        var infos = new Character[count];
        for (int i = 0; i < count; i++)
        {
            infos[i] = Deserialize(message);
        }
        return infos;
    }

    public static Message SerializeMany(Message message, Character[] characters)
    {
        message.AddUShort((ushort)characters.Length);
        foreach (var character in characters)
        {
            character.Serialize(message);
        }
        return message;
    }
}
