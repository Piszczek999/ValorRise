namespace ValorRise;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Riptide;

public class User
{
    public int FailedLoginAttempts { get; set; } = 0;
    public DateTime? LockoutEnd { get; set; }

    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("username")]
    public string Username { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }

    [BsonElement("passwordHash")]
    public string PasswordHash { get; set; }

    [BsonElement("salt")]
    public string Salt { get; set; }

    [BsonElement("roles")]
    public string[] Roles { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    public Message Serialize(Message message) => message
        .AddObjectId(Id)
        .AddString(Username)
        .AddString(Email)
        .AddString(PasswordHash)
        .AddString(Salt)
        .AddStrings(Roles)
        .AddString(CreatedAt.ToString());

    public static User Deserialize(Message message) => new()
    {
        Id = message.GetObjectId(),
        Username = message.GetString(),
        Email = message.GetString(),
        PasswordHash = message.GetString(),
        Salt = message.GetString(),
        Roles = message.GetStrings(),
        CreatedAt = DateTime.Parse(message.GetString())
    };

    public static User[] DeserializeMany(Message message)
    {
        var count = message.GetUShort();
        var users = new User[count];
        for (int i = 0; i < count; i++)
        {
            users[i] = Deserialize(message);
        }
        return users;
    }

    public static Message SerializeMany(Message message, User[] users)
    {
        message.AddUShort((ushort)users.Length);
        foreach (var user in users)
        {
            user.Serialize(message);
        }
        return message;
    }
}
