namespace ValorRise;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Riptide;

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

    public Message Serialize(Message message) => message
        .AddObjectId(Id)
        .AddObjectId(OwnerId)
        .AddString(Name)
        .AddInt(Level)
        .AddInt(Exp)
        .AddString(CreatedAt.ToString());

    public static CharacterInfo Deserialize(Message message) => new()
    {
        Id = message.GetObjectId(),
        OwnerId = message.GetObjectId(),
        Name = message.GetString(),
        Level = message.GetInt(),
        Exp = message.GetInt(),
        CreatedAt = DateTime.Parse(message.GetString())
    };

    public static CharacterInfo[] DeserializeMany(Message message)
    {
        var count = message.GetUShort();
        var infos = new CharacterInfo[count];
        for (int i = 0; i < count; i++)
        {
            infos[i] = Deserialize(message);
        }
        return infos;
    }

    public static Message SerializeMany(Message message, CharacterInfo[] characters)
    {
        message.AddUShort((ushort)characters.Length);
        foreach (var character in characters)
        {
            character.Serialize(message);
        }
        return message;
    }
}
