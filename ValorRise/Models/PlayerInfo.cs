using MongoDB.Bson;
using Riptide;

namespace ValorRise.Models;

public class CharacterInfo : IMessageSerializable
{
    public ObjectId Id { get; set; }
    public ObjectId UserId { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public DateTime CreatedAt { get; set; }

    public void Serialize(Message message) => message
        .AddObjectId(Id)
        .AddObjectId(UserId)
        .AddString(Name)
        .AddInt(Level)
        .AddString(CreatedAt.ToString());

    public void Deserialize(Message message)
    {
        Id = message.GetObjectId();
        UserId = message.GetObjectId();
        Name = message.GetString();
        Level = message.GetInt();
        CreatedAt = DateTime.Parse(message.GetString());
    }
}
