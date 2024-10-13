using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Loading.Server;

[Packet(PacketType.PlayerInfo, MessageSendMode.Reliable)]
public record PlayerInfoPacket(
    ObjectId Id,
    Vector2 Position,
    string Name,
    ushort Level,
    float Exp,
    ulong Gold,
    float Health,
    float MaxHealth,
    float Mana,
    float MaxMana) : IServerPacket
{
    public PlayerInfoPacket(Message packet) : this(
        packet.GetObjectId(),
        packet.GetVector2(),
        packet.GetString(),
        packet.GetUShort(),
        packet.GetFloat(),
        packet.GetULong(),
        packet.GetFloat(),
        packet.GetFloat(),
        packet.GetFloat(),
        packet.GetFloat())
    { }

    public void Write(Message packet) => packet
        .AddObjectId(Id)
        .AddVector2(Position)
        .AddString(Name)
        .AddUShort(Level)
        .AddFloat(Exp)
        .AddULong(Gold)
        .AddFloat(Health)
        .AddFloat(MaxHealth)
        .AddFloat(Mana)
        .AddFloat(MaxMana);
}