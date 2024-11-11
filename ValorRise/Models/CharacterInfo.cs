using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Models;

public class CharacterInfo : IMessageSerializable
{
    public ObjectId Id { get; set; }
    public ObjectId UserId { get; set; }
    public string Name { get; set; }
    public byte Level { get; set; }
    public Class Class { get; set; }
    public DateTime CreatedAt { get; set; }

    public void Serialize(Message message) => message
        .AddObjectId(Id)
        .AddObjectId(UserId)
        .AddString(Name)
        .AddByte(Level)
        .AddString(CreatedAt.ToString());

    public void Deserialize(Message message)
    {
        Id = message.GetObjectId();
        UserId = message.GetObjectId();
        Name = message.GetString();
        Level = message.GetByte();
        CreatedAt = DateTime.Parse(message.GetString());
    }

    public static CharacterInfo FromCharacter(Character character) => new CharacterInfo
    {
        Id = character.Id,
        UserId = character.UserId,
        Name = character.Name,
        Level = character.Level,
        CreatedAt = character.CreatedAt,
        Class = character.Class,
    };
}
