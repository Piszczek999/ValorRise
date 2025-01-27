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
    public byte Level { get; set; }
    public uint Experience { get; set; }
    public uint Gold { get; set; }
    public byte MapId { get; set; }
    public float Health { get; set; }
    public float Mana { get; set; }
    public SlotDB[] Inventory { get; set; }
    public SlotDB[] Hotbar { get; set; }
    public SlotDB[] Equipment { get; set; }

    public void Serialize(Message message) => message
        .AddObjectId(Id)
        .AddObjectId(UserId)
        .AddString(Name)
        .AddVector2(Position)
        .AddByte(Level)
        .AddUInt(Experience)
        .AddUInt(Gold)
        .AddByte(MapId)
        .AddFloat(Health)
        .AddFloat(Mana)
        .AddSerializables(Inventory ?? Array.Empty<SlotDB>())
        .AddSerializables(Hotbar ?? Array.Empty<SlotDB>())
        .AddSerializables(Equipment ?? Array.Empty<SlotDB>());

    public void Deserialize(Message message)
    {
        Id = message.GetObjectId();
        UserId = message.GetObjectId();
        Name = message.GetString();
        Position = message.GetVector2();
        Level = message.GetByte();
        Experience = message.GetUInt();
        Gold = message.GetUInt();
        MapId = message.GetByte();
        Health = message.GetFloat();
        Mana = message.GetFloat();
        Inventory = message.GetSerializables<SlotDB>();
        Hotbar = message.GetSerializables<SlotDB>();
        Equipment = message.GetSerializables<SlotDB>();
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
