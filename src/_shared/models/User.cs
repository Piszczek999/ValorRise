namespace MMO_Library;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


public class User
{
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
}
