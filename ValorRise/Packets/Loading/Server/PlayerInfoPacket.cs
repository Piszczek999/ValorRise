using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Loading.Server;

[Packet(PacketType.PlayerInfo, MessageSendMode.Reliable)]
public record PlayerInfoPacket(
    ObjectId Id,
    byte Level,
    uint Exp,
    uint Gold,
    float Mana,
    float MaxMana) : IServerPacket
{
    public PlayerInfoPacket(Message packet) : this(
        packet.GetObjectId(),
        packet.GetByte(),
        packet.GetUInt(),
        packet.GetUInt(),
        packet.GetFloat(),
        packet.GetFloat())
    { }

    public void Write(Message packet) => packet
        .AddObjectId(Id)
        .AddByte(Level)
        .AddUInt(Exp)
        .AddUInt(Gold)
        .AddFloat(Mana)
        .AddFloat(MaxMana);
}