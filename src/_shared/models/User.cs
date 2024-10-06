namespace ValorRise;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Riptide;

public class User : IMessageSerializable
{
    public int FailedLoginAttempts { get; set; } = 0;
    public DateTime? LockoutEnd { get; set; }

    [BsonId] public ObjectId Id { get; set; }
    [BsonElement("username")] public string Username { get; set; }
    [BsonElement("email")] public string Email { get; set; }
    [BsonElement("passwordHash")] public string PasswordHash { get; set; }
    [BsonElement("salt")] public string Salt { get; set; }
    [BsonElement("roles")] public string[] Roles { get; set; }
    [BsonElement("createdAt")] public DateTime CreatedAt { get; set; }

    public void Serialize(Message message) => message
        .AddObjectId(Id)
        .AddString(Username)
        .AddString(Email)
        .AddString(PasswordHash)
        .AddString(Salt)
        .AddStrings(Roles)
        .AddString(CreatedAt.ToString());

    public void Deserialize(Message message)
    {
        Id = message.GetObjectId();
        Username = message.GetString();
        Email = message.GetString();
        PasswordHash = message.GetString();
        Salt = message.GetString();
        Roles = message.GetStrings();
        CreatedAt = DateTime.Parse(message.GetString());
    }
}
