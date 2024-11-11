using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Models;

public class Character : IMessageSerializable
{
    public ObjectId Id { get; set; }
    public ObjectId UserId { get; set; }
    public string Name { get; set; }
    public Vector2 Position { get; set; }
    public Class Class { get; set; }
    public byte Level { get; set; }
    public uint Exp { get; set; }
    public uint Gold { get; set; }
    public byte MapId { get; set; }
    public bool IsDead { get; set; }
    public float Health { get; set; }
    public float Mana { get; set; }
    // public Spell[] Spells { get; set; }
    public DateTime CreatedAt { get; set; }

    public Character() { }
    public Character(ObjectId id, ObjectId userId, string name, Class className)
    {
        Id = id;
        UserId = userId;
        Name = name;
        Position = Vector2.Zero;
        Class = className;
        Level = 1;
        Exp = 0;
        Gold = 0;
        MapId = 0;
        IsDead = false;
        Health = 100;
        Mana = 50;
        // Spells = new Spell[4];
        CreatedAt = DateTime.UtcNow;
    }

    public void Serialize(Message message) => message
        .AddObjectId(Id)
        .AddObjectId(UserId)
        .AddString(Name)
        .AddVector2(Position)
        .AddByte((byte)Class)
        .AddByte(Level)
        .AddUInt(Exp)
        .AddUInt(Gold)
        .AddByte(MapId)
        .AddBool(IsDead)
        .AddFloat(Health)
        .AddFloat(Mana)
        // .AddSerializables(Spells)
        .AddString(CreatedAt.ToString());

    public void Deserialize(Message message)
    {
        Id = message.GetObjectId();
        UserId = message.GetObjectId();
        Name = message.GetString();
        Position = message.GetVector2();
        Class = (Class)message.GetByte();
        Level = message.GetByte();
        Exp = message.GetUInt();
        Gold = message.GetUInt();
        MapId = message.GetByte();
        IsDead = message.GetBool();
        Health = message.GetFloat();
        Mana = message.GetFloat();
        CreatedAt = DateTime.Parse(message.GetString());
    }
}

// public class Spell : IMessageSerializable
// {
//     public SpellName SpellName { get; set; }
//     public byte Level { get; set; }

//     public void Serialize(Message message) => message
//     .AddByte((byte)SpellName)
//     .AddByte(Level);

//     public void Deserialize(Message message)
//     {
//         SpellName = (SpellName)message.GetByte();
//         Level = message.GetByte();
//     }
// }
