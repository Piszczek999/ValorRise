using System.Numerics;
using MongoDB.Bson;
using Riptide;

namespace ValorRise.Models;

public class Character : IMessageSerializable
{
    public ObjectId Id { get; set; }
    public ObjectId UserId { get; set; }
    public string Name { get; set; }
    public Vector2 Position { get; set; }
    public ushort Level { get; set; }
    public float Exp { get; set; }
    public ulong Gold { get; set; }
    public ushort MapId { get; set; }
    public bool IsDead { get; set; }
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public float Mana { get; set; }
    public float MaxMana { get; set; }
    public DateTime CreatedAt { get; set; }

    public void Serialize(Message message) => message
        .AddObjectId(Id)
        .AddObjectId(UserId)
        .AddString(Name)
        .AddVector2(Position)
        .AddUShort(Level)
        .AddFloat(Exp)
        .AddULong(Gold)
        .AddUShort(MapId)
        .AddBool(IsDead)
        .AddFloat(Health)
        .AddFloat(MaxHealth)
        .AddFloat(Mana)
        .AddFloat(MaxMana)
        .AddString(CreatedAt.ToString());

    public void Deserialize(Message message)
    {
        Id = message.GetObjectId();
        UserId = message.GetObjectId();
        Name = message.GetString();
        Position = message.GetVector2();
        Level = message.GetUShort();
        Exp = message.GetFloat();
        Gold = message.GetULong();
        MapId = message.GetUShort();
        IsDead = message.GetBool();
        Health = message.GetFloat();
        MaxHealth = message.GetFloat();
        Mana = message.GetFloat();
        MaxMana = message.GetFloat();
        CreatedAt = DateTime.Parse(message.GetString());
    }
}
